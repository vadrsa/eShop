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
using Modules.Products.Interfaces;
using EntityDTO.Products;
using eShopUI.Infrastructure.Api;
using AutoMapper;
using eShop.EntityViewModels.Products;

namespace Modules.Products.WorkItems.ProductManager.ViewModels
{
    class ProductsListViewModel : ObjectListViewModel<ProductInfoDTO, ProductInfoViewModel>
    {
        #region Private Fields
        

        #endregion
        

        public ProductsListViewModel(IContainerExtension container) : base(container)
        {
        }


    }
}
