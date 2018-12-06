using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Unity;
using Prism.Regions;
using System.Windows;
using eShopUI.Infrastructure;
using eShopUI.Infrastructure.Interfaces;
using DevExpress.Xpf.NavBar;
using Modules.Products;
using eShopUI.Infrastructure.Api;
using EntityDTO.Products;
using Modules.Products.Services;
using AutoMapper;
using eShop.EntityViewModels;
using System.Diagnostics;
using Modules.Products.WorkItems.ProductManager.Views;
using Modules.Products.WorkItems.ProductManager;
using eShopUI.Infrastructure.Constants;
using Modules.Products.WorkItems.BrandManager.Views;
using Modules.Products.WorkItems;
using Modules.Products.WorkItems.BrandManager;
using Modules.Products.WorkItems.Categories.Views;
using Modules.Products.WorkItems.Categories;

namespace Modules.Products
{
    public class ProductsModule : Module
    {
        #region Private Fields

        private IContainerProvider _container;
        private IRegionManager _regionManager;

        #endregion

        #region Properties
        
        public override string Name
        {
            get
            {
                return "Products";
            }
        }

        #endregion

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);

            this._container = containerProvider;
            this._regionManager = this._container.Resolve<IRegionManager>();

            NavBarGroup group = new NavBarGroup();
            group.Header = MenuGroups.ProductManagerGroup;
            NavBarItem item1 = new NavBarItem();
            item1.Content = Constants.UIConstants.ProductManagerWorkItemName;
            item1.Command = ToSecureCommand(OpenProductManagerWorkItem);

            NavBarItem item2 = new NavBarItem();
            item2.Content = Constants.UIConstants.BarndManagerWorkItemName;
            item2.Command = ToSecureCommand(OpenBrandManagerWorkItem);

            NavBarItem item3 = new NavBarItem();
            item3.Content = Constants.UIConstants.CategoriesWorkItemName;
            item3.Command = ToSecureCommand(OpenCategoriesWorkItem);
            group.Items.Add(item1);
            group.Items.Add(item2);
            group.Items.Add(item3);

            ShellExtensionService.AddNavigationGroupExtension(group);
        }

        private void OpenProductManagerWorkItem()
        {
            CurrentContextService.LaunchWorkItem<ProductManagerWorkItem>();
        }

        private void OpenBrandManagerWorkItem()
        {
            CurrentContextService.LaunchWorkItem<BrandManagerWorkItem>();
        }

        private void OpenCategoriesWorkItem()
        {
            CurrentContextService.LaunchWorkItem<CategoriesWorkItem>();
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterSingleton<IRestInfoConsumingService<ProductInfoDTO>, ProductInfoService>();
            containerRegistry.RegisterSingleton<IRestDetailConsumingService<ProductDetailDTO>, ProductDetailService>();
            containerRegistry.Register<ProductsDetailView>();
            containerRegistry.RegisterForNavigation<ProductsListView>();

            containerRegistry.RegisterSingleton<IRestInfoConsumingService<BrandInfoDTO>, BrandInfoService>();
            containerRegistry.RegisterSingleton<IRestDetailConsumingService<BrandDetailDTO>, BrandDetailService>();
            containerRegistry.Register<BrandsDetailView>();
            containerRegistry.RegisterForNavigation<BrandsListView>();


            containerRegistry.RegisterSingleton<IRestConsumingService<CategoryDTO>, CategoryService>();
            containerRegistry.RegisterSingleton<IRestInfoConsumingService<CategoryDTO>, CategoryService>();
            containerRegistry.RegisterSingleton<IRestDetailConsumingService<CategoryDTO>, CategoryService>();
            containerRegistry.Register<CategoriesView>();
        }
        
    }
}
