using DevExpress.Xpf.Bars;
using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopUI.Infrastructure.Interfaces
{
    public interface IShellExtensionService
    {
        void ShowInContentRegion(object view);
        void RemoveFromContentRegion(object view);


        void ShowInDockRegion(object view);
        void RemoveFromDockRegion(object view);


        void AddNavigationGroupExtension(NavBarGroup group);
        void AddCommandGroupExtension(string category, string page, RibbonPageGroup group);
        void AddCommandExtension(BarButtonItem item);
        void RemoveCommandExtension(string name);
        void RemoveCommandCategory(string categoryName);
        //void AddCommandGroupExtension(string group, ControlGroupUI contolGroup, WorkItem workItem, string groupNameAfter = null);
        //void AddCommandPageExtension(DevExpress.XtraBars.Ribbon.RibbonPage page);

    }
}
