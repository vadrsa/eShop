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

namespace Modules.Brands.BrandManager.ViewModels
{
    class BrandsListViewModel : ObjectListViewModel<BrandInfoDTO, BrandInfoViewModel>
    {
        #region Private Fields
        

        #endregion
        

        public BrandsListViewModel(IContainerExtension container) : base(container)
        {
        }


    }
}
