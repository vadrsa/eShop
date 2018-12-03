using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Ribbon.Internal;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace eShopUI.Infrastructure
{
    public class NavBarRegionAdaptor : RegionAdapterBase<NavBarControl>
    {
        public NavBarRegionAdaptor(IRegionBehaviorFactory factory)
            : base(factory)
        {

        }

        protected override void Adapt(IRegion region, NavBarControl regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (object element in e.NewItems)
                    {
                        NavBarGroup group = (NavBarGroup)element;

                        if (element is NavBarGroup)
                        {
                            NavBarGroup found = regionTarget.Groups.Where(g => g.Header == group.Header).FirstOrDefault();
                            if (found != null)
                            {
                                //int index = regionTarget.Groups.IndexOf(found);
                                //regionTarget.Groups.RemoveAt(index);
                                List<NavBarItem> groupItems = new List<NavBarItem>();
                                foreach (var el in group.Items)
                                {
                                    groupItems.Add(el as NavBarItem);
                                }
                                group.Items.Clear();
                                foreach (var el in groupItems)
                                {
                                    if(el != null)
                                    found.Items.Add(el);
                                }

                            }
                            else
                                regionTarget.Groups.Add(group);

                        }

                    }
                }
                
            };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}
