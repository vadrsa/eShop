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
using Modules.Products.ProductManager.Views;
using Modules.Products.ProductManager;
using eShopUI.Infrastructure.Constants;

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
            group.Items.Add(item1);
            

            ShellExtensionService.AddNavigationGroupExtension(group);
        }

        private void OpenProductManagerWorkItem()
        {
            DateTime start = DateTime.Now;
            CurrentContextService.LaunchWorkItem<ProductManagerWorkItem>();
            TimeSpan took = DateTime.Now - start;
            Debug.WriteLine("Launch Workitem took: " + took.TotalSeconds);
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterSingleton<IRestInfoConsumingService<ProductInfoDTO>, ProductInfoService>();
            containerRegistry.RegisterSingleton<IRestDetailConsumingService<ProductDetailDTO>, ProductDetailService>();
            containerRegistry.RegisterSingleton<IRestDetailConsumingService<CategoryDTO>, CategoryService>();
            containerRegistry.Register<ProductsDetailView>();
            containerRegistry.RegisterForNavigation<ProductsListView>();
        }
        
    }
}
