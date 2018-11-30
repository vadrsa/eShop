using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Unity;
using Prism.Regions;
using Modules.Security.Views;
using DevExpress.Xpf.Core;
using System.Windows;
using DevExpress.Mvvm;
using Modules.Security.ViewModels;
using eShopUI.Infrastructure;
using Security.Services;
using eShopUI.Infrastructure.Interfaces;
using DevExpress.Xpf.Bars;
using DevExpress.Utils.Design;
using eShopUI.Infrastructure.Events;
using DXInfrastructure.Imaging;

namespace Modules.Security
{
    public class SecurityModule : Module
    {
        #region Private Fields

        private IContainerProvider _container;
        private IRegionManager _regionManager;

        #endregion


        public override void OnInitialized(IContainerProvider containerProvider)
        {
            base.OnInitialized(containerProvider);
            this._container = containerProvider;
            this._regionManager = this._container.Resolve<IRegionManager>();

            EventAggregator.GetEvent<AuthenticatedEvent>().Subscribe(HandleUserAuthenticated);

            BarButtonItem loginButton = new BarButtonItem();
            loginButton.Content = "Login";
            loginButton.Command = ToCommand(StartAuthenticationProccess);
            loginButton.Glyph = AppImageHelper.GetImageSource("BOUser", ImageSize.Size16x16);
            loginButton.LargeGlyph = AppImageHelper.GetImageSource("BOUser", ImageSize.Size32x32);
            ShellExtensionService.AddCommandExtension(loginButton);
            StartAuthenticationProccess();
        }
        
        public override string Name
        {
            get
            {
                return "Security";
            }
        }

        private void HandleUserAuthenticated()
        {
            ShellExtensionService.RemoveCommandExtension("Login");
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.Register<LoginView>();
            containerRegistry.RegisterSingleton<IAuthenticationUIService, AuthenticationUIService>();
        }
        
        private void StartAuthenticationProccess()
        {
            _container.Resolve<IAuthenticationUIService>().StartAuthentication();
            
        }
    }
}
