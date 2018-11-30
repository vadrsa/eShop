using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Ioc;
using Unity;
using Prism.Regions;
using Modules.System.Views;

namespace Modules.System
{
    public class SystemModule : IModule
    {
        #region Private Fields

        private IContainerProvider _container;
        private IRegionManager _regionManager;

        #endregion


        public void OnInitialized(IContainerProvider containerProvider)
        {
            this._container = containerProvider;
            this._regionManager = this._container.Resolve<IRegionManager>();
            StartAuthenticationProccess();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<LoginView>();
        }

        private void StartAuthenticationProccess()
        {
            var a = this._container.Resolve<LoginView>();
            this._regionManager.Regions["PopupRegion"].Add(a);
            this._regionManager.Regions["PopupRegion"].Activate(a);
        }
    }
}
