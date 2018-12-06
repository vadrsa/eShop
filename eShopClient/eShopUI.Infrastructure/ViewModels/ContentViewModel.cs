using AutoMapper;
using DevExpress.Mvvm;
using eShop.EntityViewModels;
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
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace eShopUI.Infrastructure.ViewModels
{
    public abstract class ContentViewModel<T, TViewModel> : CrudManagerViewModel
        where TViewModel : IIdEntityViewModel, IEditTracking, new()
    {


        #region Private Fields

        private CancellationTokenSource loadListTokenSource;

        private IMapper _mapper;

        private IRestConsumingService<T> _service;

        private IEventAggregator _eventAggregator;
        private Object _listItemsLock = new Object();

        #endregion

        #region Properties

        protected IMapper Mapper { get { return _mapper; } }

        protected IRestConsumingService<T> Service { get { return _service; } }

        protected IEventAggregator EventAggregator
        {
            get
            {
                return _eventAggregator;
            }
        }

        private EditMode _editMode;

        public EditMode EditMode
        {
            get { return _editMode; }
            set
            {
                _editMode = value;
                RaisePropertyChanged(nameof(IsReadOnly));
                UpdateCrudCommands();
            }
        }

        #endregion

        #region Bindable Properties

        private EditableCollection<TViewModel> _listItems = new EditableCollection<TViewModel>();

        public EditableCollection<TViewModel> ListItems
        {
            get { return _listItems; }
            set
            {

                SetProperty(ref _listItems, value, nameof(ListItems));

                ExecuteUIThread(() => {
                    BindingOperations.EnableCollectionSynchronization(_listItems, _listItemsLock);
                });
                if (_listItems != null && _listItems.Count > 0)
                    CurrentItem = _listItems.First();
            }
        }

        private TViewModel _currentItem;
        public TViewModel CurrentItem
        {
            get { return _currentItem; }
            set
            {
                SetProperty(ref _currentItem, value, nameof(CurrentItem));

            }
        }

        private TViewModel _focusedRow;
        public TViewModel FocusedRow
        {
            get { return _focusedRow; }
            set
            {
                SetProperty(ref _focusedRow, value, nameof(FocusedRow));

            }
        }

        private bool _IsListLoading;

        public bool IsListLoading
        {
            get { return _IsListLoading; }
            set
            {
                SetProperty(ref _IsListLoading, value, nameof(IsListLoading));
                RaisePropertyChanged(nameof(IsEnabledExtended));
                RaisePropertyChanged(nameof(IsReadOnly));
                UpdateCrudCommands();
            }
        }

        private bool _IsEnabled = true;

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                SetProperty(ref _IsEnabled, value, nameof(IsEnabled));
                RaisePropertyChanged(nameof(IsEnabledExtended));
            }
        }

        private bool _IsReadOnly;

        public bool IsReadOnly
        {
            get { return EditMode == EditMode.ReadOnly || IsListLoading; }
        }

        public bool IsEnabledExtended
        {
            get { return !IsListLoading && IsEnabled; }
        }

        #endregion

        #region Private/Protected Methods

        protected override bool CanEdit()
        {
            return EditMode == EditMode.ReadOnly && ListItems != null && !IsListLoading;
        }
        protected override void Edit()
        {
            ListItems.BeginEdit();
            EditMode = EditMode.Editable;
        }

        protected override bool CanSave()
        {
            return EditMode == EditMode.Editable && ListItems != null && !IsListLoading;
        }

        protected virtual List<T> GetNewItems()
        {
            return Mapper.Map<List<T>>(ListItems.New.ToList());
        }

        protected override void Save()
        {
            IsListLoading = true;
            WorkCurrentItemBinding();
            List<T> newItems = GetNewItems();
            List<T> delItems = Mapper.Map<List<T>>(ListItems.Deleted.ToList());
            List<T> changedItems = Mapper.Map<List<T>>(ListItems.Changed.ToList());
            TrackableContainer<T> trackableContainer = new TrackableContainer<T>(changedItems, newItems, delItems);
            DoSaveEditable(trackableContainer).Subscribe((res) => {
                if (res)
                {
                    ListItems.EndEdit();
                    EditMode = EditMode.ReadOnly;
                }
                else
                    ShowMessageBox("Error occured while saving data.", "Error", System.Windows.MessageBoxButton.OK);
                IsListLoading = false;
            }, (e) => {
                ShowMessageBox("Error occured while saving data.", "Error", System.Windows.MessageBoxButton.OK);
                IsListLoading = false;

            });

        }

        protected void ListItemsRemoveRange(List<TViewModel> list)
        {
            foreach(var item in list)
            {
                _listItems.Remove(item);
            }
            RaisePropertiesChanged(nameof(ListItems));
        }

        protected abstract IObservable<bool> DoSaveEditable(TrackableContainer<T> trackableContainer);

        private void WorkCurrentItemBinding()
        {
            var temp = CurrentItem;
            CurrentItem = default(TViewModel);
            CurrentItem = temp;
        }

        protected override void CancelEditing()
        {
            WorkCurrentItemBinding();
            ListItems.CancelEdit();
            EditMode = EditMode.ReadOnly;
        }

        protected override bool CanCancelEditing()
        {
            return EditMode == EditMode.Editable && !IsListLoading;
        }

        public override void Cleanup()
        {
            base.Cleanup();
            this.loadListTokenSource?.Cancel();
        }
        private void SetCurrentItemInternal(TViewModel item)
        {
            SetProperty(ref _currentItem, item, nameof(CurrentItem));
        }

        protected virtual void HandleEditingModeChanged(EditMode mode)
        {
            IsEnabled = mode == EditMode.ReadOnly;
        }

        private void StartLoadList()
        {

            if (loadListTokenSource != null)
            {
                loadListTokenSource.Cancel();
            }
            IsListLoading = true;
            
            loadListTokenSource = new CancellationTokenSource();
            IObservable<EditableCollection<TViewModel>> observable =  GetListObservable();
            observable.Subscribe((list) => {
                ListItems = list;
                IsListLoading = false;
            }, (e) => {
                ShowMessageBox("Error occured while fetching data.", "Error", System.Windows.MessageBoxButton.OK);
                IsListLoading = false;
            }, loadListTokenSource.Token);
        }

        private IObservable<EditableCollection<TViewModel>> GetListObservable()
        {

            return Observable.FromAsync(() => _service.Get()).Select(list => Mapper.Map<EditableCollection<TViewModel>>(list));

        }

        #endregion

        
        public ContentViewModel(IContainerExtension container)
        {

            _eventAggregator = container.Resolve<IEventAggregator>();
            _service = container.Resolve<IRestConsumingService<T>>();
            _mapper = container.Resolve<IMapper>();
            StartLoadList();
        }

    }
}
