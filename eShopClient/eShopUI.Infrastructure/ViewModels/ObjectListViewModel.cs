using AutoMapper;
using DevExpress.Mvvm;
using eShop.EntityViewModels;
using eShop.EntityViewModels.Interfaces;
using eShopUI.Infrastructure.Enums;
using eShopUI.Infrastructure.Events;
using eShopUI.Infrastructure.Interfaces;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace eShopUI.Infrastructure.ViewModels
{
    public abstract class ObjectListViewModel<T, TInfoViewModel> : CrudManagerViewModel
        where TInfoViewModel : IIdEntityViewModel, new()
    {


        #region Private Fields

        private CancellationTokenSource loadListTokenSource;

        private IMapper _mapper;

        private IRestInfoConsumingService<T> _service;

        private IEventAggregator _eventAggregator;

        private Subject<TInfoViewModel> _whenCurrentItemChanges;

        #endregion

        #region Properties

        protected IMapper Mapper { get { return _mapper; } }

        protected IRestInfoConsumingService<T> Service { get { return _service; } }

        protected IEventAggregator EventAggregator
        {
            get
            {
                return _eventAggregator;
            }
        }

        #endregion

        #region Bindable Properties

        private ObservableCollection<TInfoViewModel> _listItems = new ObservableCollection<TInfoViewModel>();

        public ObservableCollection<TInfoViewModel> ListItems
        {
            get { return _listItems; }
            set { SetProperty(ref _listItems, value, nameof(ListItems));  }
        }

        private TInfoViewModel _currentItem;
        public TInfoViewModel CurrentItem
        {
            get { return _currentItem; }
            set
            {
                bool changed = (value == null && _currentItem != null) || (value != null && !(value.Equals(_currentItem)));
                SetProperty(ref _currentItem, value, nameof(CurrentItem));
                if (changed)
                    _whenCurrentItemChanges.OnNext(value);

            }
        }

        private bool _IsListLoading;

        public bool IsListLoading
        {
            get { return _IsListLoading; }
            set
            {
                SetProperty(ref _IsListLoading, value, nameof(IsListLoading));
                UpdateCrudCommands();
                RaisePropertyChanged(nameof(IsEnabledExtended));
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
        
        public bool IsEnabledExtended
        {
            get { return !IsListLoading && IsEnabled; }
        }

        #endregion


        #region Private/Protected Methods
        private void SetCurrentItemInternal(TInfoViewModel item)
        {
            SetProperty(ref _currentItem, item, nameof(CurrentItem));
        }


        protected override bool CanDelete()
        {
            return CurrentItem != null && !IsListLoading;
        }

        protected override void Delete()
        {
            IsListLoading = true;
            IObservable<bool> obs = Observable.FromAsync(() => Service.Delete(Mapper.Map<T>(CurrentItem)));
            obs.Subscribe((res) => {
                if (res)
                {
                    int index = ListItems.IndexOf(CurrentItem);
                    ExecuteUIThread(() =>
                    {
                        ListItems.RemoveAt(index);
                    });
                    if (ListItems.Count > 0)
                    {
                        CurrentItem = ListItems[Math.Max(0, index - 1)];
                    }
                }
                IsListLoading = false;
            });
        }

        protected override bool CanEdit()
        {
            return ListItems != null && !IsListLoading;
        }


        protected override bool CanAdd()
        {
            return ListItems != null && !IsListLoading;
        }
        
        protected virtual void HandleRowChanged(TInfoViewModel obj)
        {
            if (obj != null)
                EventAggregator.GetEvent<ObjectListViewRowChanged>().Publish(obj.ID);
        }

        protected virtual void HandleEditingModeChanged(EditMode mode)
        {
            IsEnabled = mode == EditMode.ReadOnly;
        }

        private void ObjectDetailsUpdated(object obj)
        {
            try
            {
                TInfoViewModel info = Mapper.Map<TInfoViewModel>(obj);
                if (ListItems == null) return;
                for (int i = 0; i < ListItems.Count; i++)
                {
                    if (ListItems[i].ID == info.ID)
                    {
                        ExecuteUIThread(() => Mapper.Map(obj, ListItems[i]));
                        return;
                    }
                }
            }
            catch {
            }
        }


        private void ObjectDetailsItemAdded(object obj)
        {
            try
            {
                TInfoViewModel info = Mapper.Map<TInfoViewModel>(obj);
                ExecuteUIThread(() => {
                    if (ListItems == null) ListItems = new ObservableCollection<TInfoViewModel>();
                    ListItems.Add(info);
                });
                //CurrentItem = info;
            }
            catch(Exception e)
            {
            }
        }

        private void StartLoadList()
        {

            if (loadListTokenSource != null)
            {
                loadListTokenSource.Cancel();
            }
            IsListLoading = true;

            loadListTokenSource = new CancellationTokenSource();
            IObservable<ObservableCollection<TInfoViewModel>> observable =  GetListObservable();
            observable.Subscribe((list) => {
                ListItems = list;
                IsListLoading = false;
            }, (e) => {
                ShowMessageBox("Error occured while fetching data.", "Error", System.Windows.MessageBoxButton.OK);
                IsListLoading = false;
            }, loadListTokenSource.Token);
        }

        private IObservable<ObservableCollection<TInfoViewModel>> GetListObservable()
        {

            return Observable.FromAsync(() => _service.Get()).Select(list => Mapper.Map<ObservableCollection<TInfoViewModel>>(list));

        }

        #endregion

#if DESIGN
        public ObjectListViewModel()
        {
            ListItems = new ObservableCollection<TInfoViewModel>();
        }
#endif
        public ObjectListViewModel(IContainerExtension container)
        {
            _eventAggregator = container.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<ObjectDetailViewEditingModeChanged>().Subscribe(HandleEditingModeChanged);
            _service = container.Resolve<IRestInfoConsumingService<T>>();
            _mapper = container.Resolve<IMapper>();
            Tokens.Add(EventAggregator.GetEvent<ObjectDetailsUpdatedEvent>().Subscribe(ObjectDetailsUpdated));
            Tokens.Add(EventAggregator.GetEvent<ObjectDetailsItemAddedEvent>().Subscribe(ObjectDetailsItemAdded));
            StartLoadList();
            _whenCurrentItemChanges = new Subject<TInfoViewModel>();
            _whenCurrentItemChanges.Throttle(TimeSpan.FromSeconds(0.2)).Subscribe((obj) => HandleRowChanged(obj));
        }

    }
}
