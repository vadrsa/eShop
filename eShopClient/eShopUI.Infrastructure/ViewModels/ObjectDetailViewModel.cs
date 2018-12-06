using AutoMapper;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using eShop.EntityViewModels.Interfaces;
using eShopUI.Infrastructure.Enums;
using eShopUI.Infrastructure.Events;
using eShopUI.Infrastructure.Interfaces;
using ModelChangeTracking;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace eShopUI.Infrastructure.ViewModels
{
    public class ObjectDetailViewModel<T, TDetailViewModel> : CrudManagerViewModel
        where TDetailViewModel : IIdEntityViewModel, INotifyPropertyChanged, new()
    {

        #region Private Fields
        private TDetailViewModel _oldObject;
        private IContainerExtension _container;
        private IMapper _mapper;

        private IRestDetailConsumingService<T> _service;

        private IEventAggregator _eventAggregator;

        private EditableAdapter<TDetailViewModel> editableObject;

        private CancellationTokenSource getObjectTokenSource;

        private int _lastRowID;
        #endregion

        #region Properties
        
        protected IEventAggregator EventAggregator { get { return _eventAggregator; } }

        protected IMapper Mapper { get { return _mapper; } }

        protected IRestDetailConsumingService<T> Service { get { return _service; } }

        private EditMode _editMode;

        public EditMode EditMode
        {
            get { return _editMode; }
            set {
                _editMode = value;
                RaisePropertiesChanged(nameof(IsReadOnly));
                EventAggregator.GetEvent<ObjectDetailViewEditingModeChanged>().Publish(EditMode);
                UpdateCrudCommands();
            }
        }


        #endregion
        

        public ObjectDetailViewModel(IContainerExtension container)
        {
            _container = container;
            _eventAggregator = container.Resolve<IEventAggregator>();
            Tokens.Add(_eventAggregator.GetEvent<ObjectListViewRowChanged>().Subscribe(HandleListViewRowChanged));
            _service = container.Resolve<IRestDetailConsumingService<T>>();
            _mapper = container.Resolve<IMapper>();
        }

        #region Events
        

        #endregion

        #region Bindable Properties

        private TDetailViewModel _Object;

        public TDetailViewModel Object
        {
            get { return _Object; }
            set {
                SetProperty(ref _Object, value, nameof(Object));
                UpdateCrudCommands();
                if (_Object == null)
                    Width = 0;
                else
                    Width = Double.NaN;
            }
        }


        private double _Width;
        public double Width
        {
            get { return _Width; }
            set
            {
                SetProperty(ref _Width, value, nameof(Width));
            }
        }

        private bool _IsReadOnly;
        public bool IsReadOnly
        {
            get { return EditMode == EditMode.ReadOnly || IsObjectLoading; }
        }

        private bool _IsObjectLoading;

        public bool IsObjectLoading
        {
            get { return _IsObjectLoading; }
            set { SetProperty(ref _IsObjectLoading, value, nameof(IsObjectLoading));
                UpdateCrudCommands();
                RaisePropertiesChanged(nameof(IsReadOnly));

            }
        }


        #endregion

        #region Public/Protected Methods

        public override void Cleanup()
        {
            base.Cleanup();
            this.getObjectTokenSource?.Cancel();
        }

        protected virtual bool IsValid()
        {
            if(Object is IDataErrorInfoExtended)
            {
                var errorInfo = (IDataErrorInfoExtended)Object;
                return !errorInfo.HasErrors;
            }
            return true;
        }

        protected bool Validate()
        {
            bool isValid = IsValid();
            if (!isValid)
                ShowMessageBox("Please correct validation errors.", "Validation Failed.", MessageBoxButton.OK);
            return IsValid();
        }


        protected virtual void HandleListViewRowChanged(int id)
        {
            _lastRowID = id;
            if(getObjectTokenSource != null)
            {
                getObjectTokenSource.Cancel();
            }
            getObjectTokenSource = new CancellationTokenSource();

            IsObjectLoading = true;
            IObservable<TDetailViewModel> observable = GetObservableObjectByID(id);
            observable.Subscribe((res) =>
            {
                Object = BeforeObjectLoad(res, getObjectTokenSource.Token);
                IsObjectLoading = false;
            }, (e) => {
                ShowMessageBox("Error occured while fetching data. Exception:" + e, "Faulted", System.Windows.MessageBoxButton.OK);
                IsObjectLoading = false;

            }, getObjectTokenSource.Token);
            
        }

        protected virtual TDetailViewModel BeforeObjectLoad(TDetailViewModel obj, CancellationToken token = new CancellationToken())
        {
            return obj;
        }

        private IObservable<TDetailViewModel> GetObservableObjectByID(int id)
        {
            IObservable<TDetailViewModel> observable = Observable.FromAsync(() => _service.GetByID(id))
                                                                 .Select((o) => Mapper.Map<TDetailViewModel>(o));
            return observable;
        }

        protected override void Add()
        {
            getObjectTokenSource?.Cancel();
            IsObjectLoading = false;
            _oldObject = Object;
            Object = new TDetailViewModel();
            EditMode = EditMode.Editable;
        }

        protected virtual void OnAdd()
        {
            EventAggregator.GetEvent<ObjectDetailsItemAddedEvent>().Publish(Object);
        }

        protected override bool CanAdd()
        {
            return EditMode == EditMode.ReadOnly && Object != null;
        }

        protected override void Save()
        {
            if (!Validate()) return;
            IsObjectLoading = true;
            if (Object.ID == 0)
            {
                IObservable<T> addObs = Observable.FromAsync(() => Service.Add(Mapper.Map<TDetailViewModel, T>(Object)));

                addObs.Select((o) => Mapper.Map<TDetailViewModel>(o)).Subscribe((res) => {
                    Object = res;
                    OnAdd();
                    EditMode = EditMode.ReadOnly;
                    IsObjectLoading = false;

                }, (e) => {
                    ShowMessageBox("Error occured while adding data.", "Error", System.Windows.MessageBoxButton.OK);
                    IsObjectLoading = false;

                });
            }
            else
            {
                IObservable<T> updateObs = Observable.FromAsync(() => Service.Update(Mapper.Map<TDetailViewModel, T>(Object)));
                updateObs.Subscribe((res) => {
                    editableObject.EndEdit();
                    editableObject = null;
                    Object = Mapper.Map<TDetailViewModel>(res);
                    OnSave();
                    EditMode = EditMode.ReadOnly;
                    IsObjectLoading = false;

                }, (e) => {
                    ShowMessageBox("Error occured while saving data.", "Error", System.Windows.MessageBoxButton.OK);
                    IsObjectLoading = false;

                });
            }
        }

        protected virtual void OnSave()
        {
            EventAggregator.GetEvent<ObjectDetailsUpdatedEvent>().Publish(Object);
        }

        protected override bool CanSave()
        {
            return Object != null && EditMode == EditMode.Editable && !IsObjectLoading;
        }

        protected override void Edit()
        {
            editableObject = new EditableAdapter<TDetailViewModel>(Object);
            editableObject.BeginEdit();
            EditMode = EditMode.Editable;
        }

        protected override bool CanEdit()
        {
            return Object != null && EditMode == EditMode.ReadOnly && !IsObjectLoading;
        }

        protected override void CancelEditing()
        {
            if (editableObject != null)
                editableObject.CancelEdit();
            else if (Equals(_oldObject, default(TDetailViewModel)))
            {
                Object = default(TDetailViewModel);
                HandleListViewRowChanged(_lastRowID);
            }
            else
            {
                if (_oldObject.ID == _lastRowID)
                    Object = _oldObject;
                else
                {
                    Object = default(TDetailViewModel);
                    HandleListViewRowChanged(_lastRowID);
                }

            }
            _oldObject = default(TDetailViewModel);
            editableObject = null;
            EditMode = EditMode.ReadOnly;
        }

        protected override bool CanCancelEditing()
        {
            return EditMode == EditMode.Editable && !IsObjectLoading;
        }

        protected override bool CanDelete()
        {
            return EditMode == EditMode.ReadOnly && !IsObjectLoading;
        }


        #endregion
    }
}
