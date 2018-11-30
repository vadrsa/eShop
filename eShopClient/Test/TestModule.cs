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
using DevExpress.Xpf.NavBar;
using eShopUI.Infrastructure.Interfaces;
using Modules.Test.Messages;
using Modules.Test.Messages.Views;
using Modules.Test.Interfaces;
using Modules.Test.Services;

namespace Modules.Test
{
    public class TestModule : Module
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

            NavBarGroup group = new NavBarGroup();
            group.Header = "Test Group";
            NavBarItem item1 = new NavBarItem();
            item1.Content = "Messages";
            item1.Command = ToSecureCommand(OpenMesagesWorkItem);
            group.Items.Add(item1);
            

            ShellExtensionService.AddNavigationGroupExtension(group);
        }

        public override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterTypes(containerRegistry);
            containerRegistry.RegisterForNavigation<MessagesListView>();
            containerRegistry.RegisterForNavigation<MessagesDetailView>();
            containerRegistry.RegisterSingleton<MessageWorkItem>();
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
        }

        private void OpenMesagesWorkItem()
        {
            CurrentContextService.LaunchWorkItem<MessageWorkItem>();
        }
    }
}
