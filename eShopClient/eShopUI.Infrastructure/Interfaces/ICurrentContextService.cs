using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopUI.Infrastructure.Interfaces
{
    public interface ICurrentContextService
    {
        void LaunchWorkItem<T>() where T : Workitem;
        void CloseCurrentWorkItem();
    }
}
