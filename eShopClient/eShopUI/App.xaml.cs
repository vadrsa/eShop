using CommonServiceLocator;
using eShopUI.Views;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Prism.Modularity;
using Prism.Regions;
using eShopUI.Infrastructure.Behaviors;
using eShopUI.Infrastructure.Interfaces;
using Modules.Security;
using EntityDTO;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using eShopUI.Infrastructure;
using DevExpress.Xpf.NavBar;
using eShopUI.Infrastructure.Services;
using Modules.Products;
using EntityDTO.Users;
using AutoMapper;
using EntityDTO.Products;
using eShop.EntityViewModels;
using eShop.EntityViewModels.Products;

namespace eShopUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication, IApplication
    {
        public UserDTO User { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            //ApplicationThemeHelper.ApplicationThemeName = "Caramel";
            base.OnStartup(e);
        }

        private void RegisterMapperConfiguration(IContainerRegistry containerRegistry)
        {

            MapperConfiguration config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ProductInfoDTO, ProductInfoViewModel>();
                cfg.CreateMap<ProductDetailDTO, ProductDetailViewModel>();
                cfg.CreateMap<ProductDetailViewModel, ProductInfoViewModel>();
                cfg.CreateMap<CategoryTreeItemDTO, CategoryTreeItemViewModel>();
                cfg.CreateMap<CategoryTreeItemViewModel, CategoryTreeItemDTO>();
                cfg.CreateMap<BrandInfoDTO, BrandInfoViewModel>();
                cfg.AllowNullCollections = true;
            });
            config.AssertConfigurationIsValid();
            containerRegistry.RegisterInstance<IMapper>(config.CreateMapper());
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<LoginView>("nav");
            this.RegisterMapperConfiguration(containerRegistry);
            containerRegistry.RegisterSingleton(typeof(IShellExtensionService), typeof(ShellExtensionService));
            containerRegistry.RegisterSingleton(typeof(ICurrentContextService), typeof(CurrentContextService));
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(RibbonControl), Container.Resolve<RibbonControlRegionAdaptor>());
            regionAdapterMappings.RegisterMapping(typeof(NavBarControl), Container.Resolve<NavBarRegionAdaptor>());
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<SecurityModule>();
            moduleCatalog.AddModule<ProductsModule>();
        }

        protected override Window CreateShell()
        {

            DevExpress.Xpf.Core.ApplicationThemeHelper.ApplicationThemeName =
           DevExpress.Xpf.Core.Theme.Office2016ColorfulSEName;
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);
            shell.Show();

        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
            //regionBehaviors.AddIfMissing("popupBehavior", typeof(WindowDialogActivationBehavior));
        }
    }
}
