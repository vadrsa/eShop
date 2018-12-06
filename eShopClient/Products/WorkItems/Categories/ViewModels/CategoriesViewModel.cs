using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Events;
using Prism.Ioc;
using eShopUI.Infrastructure.ViewModels;
using eShop.EntityViewModels;
using EntityDTO.Products;
using eShopUI.Infrastructure.Api;
using AutoMapper;
using eShop.EntityViewModels.Products;
using eShopUI.Infrastructure;
using Modules.Products.Services;
using System.Reactive.Linq;
using ModelChangeTracking;

namespace Modules.Products.WorkItems.Categories.ViewModels
{
    class CategoriesViewModel : ContentViewModel<CategoryDTO, CategoryViewModel>
    {
        #region Private Fields

        private int lastSmallestID;

        #endregion


        private DelegateCommand _addNewRowCommand;

        public DelegateCommand AddNewRowCommand
        {
            get
            {
                if (_addNewRowCommand == null)
                    _addNewRowCommand = new DelegateCommand(AddNewRow, CanAddNewRow);
                return _addNewRowCommand;
            }
        }

        private DelegateCommand _deleteRowCommand;

        public DelegateCommand DeleteRowCommand
        {
            get
            {
                if (_deleteRowCommand == null)
                    _deleteRowCommand = new DelegateCommand(DeleteRow, CanDeleteRow);
                return _deleteRowCommand;
            }
        }

        private void GetRowChildren(CategoryViewModel obj, List<CategoryViewModel> res)
        {
            res.Add(obj);
            foreach (var item in ListItems)
            {
                if (item.ParentID == obj.ID)
                {
                    GetRowChildren(item, res);
                }
            }
        }

        private void DeleteRow()
        {
            List<CategoryViewModel> toRemove = new List<CategoryViewModel>();
            GetRowChildren(FocusedRow, toRemove);
            ListItemsRemoveRange(toRemove);
        }

        private bool CanDeleteRow()
        {
            return FocusedRow != null && FocusedRow.ID != 0 && !IsReadOnly;
        }

        private void AddNewRow()
        {
            int id = FocusedRow.ID;
            CategoryViewModel evm = new CategoryViewModel() { ID = --lastSmallestID, ParentID = id };
            ListItems.Add(evm);
           
        }

        private bool CanAddNewRow()
        {
            return FocusedRow != null && FocusedRow.ID != 0 && !IsReadOnly;
        }

        //protected override List<CategoryDTO> GetNewItems()
        //{
        //    List<CategoryDTO> res = base.GetNewItems();
        //    foreach(var item in res)
        //    {
        //        if (item.ID < 0) ReplaceID(res, item, 0);
        //    }
        //    return res;
        //}

        private void ReplaceID(List<CategoryDTO> res, CategoryDTO toReplace , int newID)
        {
            foreach (var item in res.Where(c => c.ParentID == toReplace.ID)){
                item.ParentID = newID;
            }
            toReplace.ID = newID;

        }

        protected override IObservable<bool> DoSaveEditable(TrackableContainer<CategoryDTO> trackableContainer)
        {
            var service = (CategoryService)Service;
            return Observable.FromAsync(() => service.SaveTrackableList(trackableContainer));
        }

        public CategoriesViewModel(IContainerExtension container) : base(container)
        {
        }


    }
}
