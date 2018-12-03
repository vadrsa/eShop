using eShopUI.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.NavBar;
using Prism.Regions;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using eShopUI.Infrastructure.Constants;

namespace eShopUI.Infrastructure.Services
{
    public class ShellExtensionService : IShellExtensionService
    {
        private IRegionManager _regionManager;
        public ShellExtensionService(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }

        public void AddCommandGroupExtension(string categoryName, string pageName, RibbonPageGroup group)
        {
            RibbonPageCategory category = new RibbonPageCategory();
            category.Caption = categoryName;
            RibbonPage page = new RibbonPage();
            page.Caption = pageName;
            page.Groups.Add(group);
            category.Pages.Add(page);
            this._regionManager.AddToRegion(RegionNames.RibbonRegion, category);
        }

        public void AddCommandExtension(BarButtonItem item)
        {
            //RibbonDefaultPageCategory category = new RibbonDefaultPageCategory();
            //RibbonPage page = new RibbonPage();
            //page.Caption = UIConstants.MainCommandPageName;
            //RibbonPageGroup group = new RibbonPageGroup();
            //group.Caption = UIConstants.MainCommandGroupName;
            //group.ItemLinks.Add(item);
            //page.Groups.Add(group);
            //category.Pages.Add(page);
            this._regionManager.AddToRegion(RegionNames.RibbonRegion, item);
        }

        public void RemoveCommandExtension(string itemName)
        {
            BarButtonItem button = this._regionManager.Regions[RegionNames.RibbonRegion].Views
             .Where(v => v is BarButtonItem && (v as BarButtonItem).Content.ToString() == itemName)
             .FirstOrDefault() as BarButtonItem;
            if(button != null)
                this._regionManager.Regions[RegionNames.RibbonRegion].Remove(button);

        }

        public void RemoveCommandCategory(string categoryName)
        {
            RibbonPageCategory category = this._regionManager.Regions[RegionNames.RibbonRegion].Views
                .Where(v => v is RibbonPageCategory && (v as RibbonPageCategory).Caption == categoryName)
                .FirstOrDefault() as RibbonPageCategory;
            if(category != null)
                this._regionManager.Regions[RegionNames.RibbonRegion].Remove(category);
        }

        public void AddNavigationGroupExtension(NavBarGroup group)
        {

            this._regionManager.AddToRegion(RegionNames.NavigationRegion, group);
        }

        public void ShowInContentRegion(object view)
        {
            IRegion region = this._regionManager.Regions[RegionNames.ContentRegion];
            string viewName = view.GetType().FullName;
            if (region.GetView(viewName) != null) return;
            region.Add(view, viewName);
            region.Activate(view);
        }

        public void RemoveFromContentRegion(object view)
        {
            IRegion region = this._regionManager.Regions[RegionNames.ContentRegion];
            string viewName = view.GetType().FullName;
            object viewReal = region.GetView(viewName);
            if (viewReal == null) return;
            region.Remove(viewReal);
        }

        public void ShowInDockRegion(object view)
        {
            IRegion region = this._regionManager.Regions[RegionNames.DockRegion];
            string viewName = view.GetType().FullName;
            if (region.GetView(viewName) != null) return;
            region.Add(view, viewName);
            region.Activate(view);
        }

        public void ShowInDockRegion<T>()
        {
            IRegion region = this._regionManager.Regions[RegionNames.DockRegion];
            region.RequestNavigate(typeof(T).Name, (r)=> {
                
            });
            //return (T)region.ActiveViews.First();
        }

        public void RemoveFromDockRegion(object view)
        {
            IRegion region = this._regionManager.Regions[RegionNames.DockRegion];
            string viewName = view.GetType().FullName;
            object viewReal = region.GetView(viewName);
            if (viewReal == null) return;
            region.Remove(viewReal);
        }
    }
}
