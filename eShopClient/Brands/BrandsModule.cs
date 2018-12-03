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
using Modules.Brands.Constants;
using Modules.Brands.BrandManager.Views;
using eShopUI.Infrastructure.Constants;

namespace Modules.Brands
{
    public class BrandsModule : Module
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
                return "Brand Manager";
            }
        }

        #endregion

        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);

            this._container = containerProvider;
            this._regionManager = this._container.Resolve<IRegionManager>();

            NavBarGroup group = new NavBarGroup();
            group.Header = MenuGroups.BrandManagerGroup;
            NavBarItem item1 = new NavBarItem();
            item1.Content = Constants.UIConstants.BarndManagerWorkItemName;
            item1.Command = ToSecureCommand(OpenBrandManagerWorkItem);
            group.Items.Add(item1);
            

            ShellExtensionService.AddNavigationGroupExtension(group);
        }

        private void OpenBrandManagerWorkItem()
        {
            DateTime start = DateTime.Now;
            CurrentContextService.LaunchWorkItem<BrandManagerWorkItem>();
            TimeSpan took = DateTime.Now - start;
            Debug.WriteLine("Launch Workitem took: " + took.TotalSeconds);
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterSingleton<IRestInfoConsumingService<BrandInfoDTO>, BrandInfoService>();
            containerRegistry.RegisterSingleton<IRestDetailConsumingService<BrandDetailDTO>, BrandDetailService>();
            containerRegistry.Register<BrandsDetailView>();
            containerRegistry.RegisterForNavigation<BrandsListView>();
        }
        
    }
}
