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
                        if(element is NavBarGroup)
                            regionTarget.Groups.Add((NavBarGroup)element);
                        
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
