using eShopUI.Infrastructure;
using eShopUI.Infrastructure.Interfaces;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopUI.Infrastructure.Services
{
    public class CurrentContextService : ICurrentContextService
    {
        private IContainerExtension Container { get; set; }
        public CurrentContextService(IContainerExtension container)
        {
            Container = container;
        }

        private Workitem _currentWorkItem;

        public void CloseCurrentWorkItem()
        {
            _currentWorkItem.Terminate();
            _currentWorkItem = null;
        }

        public void LaunchWorkItem<T>() where T: Workitem
        {
            if (_currentWorkItem is T) return;
            Workitem workitem = Container.Resolve<T>();
            _currentWorkItem?.Terminate();
            _currentWorkItem = workitem;
            workitem.Run();
        }
    }
}
