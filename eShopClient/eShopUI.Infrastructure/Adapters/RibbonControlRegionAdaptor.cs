using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Ribbon.Internal;
using eShopUI.Infrastructure.Constants;
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
    public class RibbonControlRegionAdaptor : RegionAdapterBase<RibbonControl>
    {
        public RibbonControlRegionAdaptor(IRegionBehaviorFactory factory)
            : base(factory)
        {

        }

        protected override void Adapt(IRegion region, RibbonControl regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (object element in e.NewItems)
                    {
                        if (element is RibbonPageCategory)
                        {
                            RibbonPageCategory category = (RibbonPageCategory)element;
                            if (category.Pages.Count == 0) return;

                            RibbonPageCategory realCategory = regionTarget.Items.Where(item =>
                            {
                                if (item is RibbonPageCategory && ((RibbonPageCategory)item).Caption == category.Caption)
                                    return true;
                                return false;
                            }).FirstOrDefault() as RibbonPageCategory;
                            if (realCategory != null)
                            {
                                RibbonPage page = category.Pages.First();
                                RibbonPage realPage = realCategory.Pages.Where(item =>
                                {
                                    if (item.Caption.ToString() == page.Caption.ToString())
                                        return true;
                                    return false;
                                }).FirstOrDefault() as RibbonPage;
                                if (realPage != null)
                                {
                                    realCategory.Pages.Remove(realPage);

                                    foreach (var group in page.Groups)
                                    {
                                        realPage.Groups.Add(group);
                                    }

                                    realCategory.Pages.Add(realPage);
                                    regionTarget.SelectedPage = realPage;

                                }
                                else
                                {
                                    realCategory.Pages.Add(page);
                                    regionTarget.SelectedPage = page;
                                }
                            }
                            else
                            {
                                regionTarget.Items.Add(category);
                                regionTarget.SelectedPage = category.Pages.First();
                            }
                        }
                        else if (element is BarButtonItem)
                        {
                            
                            RibbonDefaultPageCategory realCategory = regionTarget.Items.Where(item => item is RibbonDefaultPageCategory).First() as RibbonDefaultPageCategory;
                            RibbonPage realPage = realCategory.Pages.Where(item =>
                            {
                                if (item.Caption.ToString() == UIConstants.MainCommandPageName)
                                    return true;
                                return false;
                            }).FirstOrDefault() as RibbonPage;
                            RibbonPageGroup realGroup = realPage.Groups.Where(item =>
                            {
                                if (item.Caption.ToString() == UIConstants.MainCommandGroupName)
                                    return true;
                                return false;
                            }).FirstOrDefault() as RibbonPageGroup;
                            if (realGroup != null)
                            {
                                int index = realPage.Groups.IndexOf(realGroup);
                                realPage.Groups.RemoveAt(index);
                                realGroup.ItemLinks.Add((BarButtonItem)element);
                                realPage.Groups.Insert(index, realGroup);
                            }
                            else
                            {
                                RibbonPageGroup group = new RibbonPageGroup();
                                group.Caption = UIConstants.MainCommandGroupName;
                                group.ItemLinks.Add((BarButtonItem)element);
                                realPage.Groups.Add(group);
                            }
                        }
                        else if (element is RibbonDefaultPageCategory)
                        {
                            RibbonDefaultPageCategory category = (RibbonDefaultPageCategory)element;
                            RibbonDefaultPageCategory realCategory = regionTarget.Items.Where(item => item is RibbonDefaultPageCategory).First() as RibbonDefaultPageCategory;
                            RibbonPage page = category.Pages.First();
                            if (page.Groups.Count == 0) return;
                            RibbonPage realPage = realCategory.Pages.Where(item =>
                            {
                                if (item.Caption.ToString() == page.Caption.ToString())
                                    return true;
                                return false;
                            }).FirstOrDefault() as RibbonPage;
                            RibbonPageGroup group = page.Groups.First();
                            if (group.ItemLinks.Count == 0) return;
                            RibbonPageGroup realGroup = realPage.Groups.Where(item =>
                            {
                                if (item.Caption.ToString() == page.Caption.ToString())
                                    return true;
                                return false;
                            }).FirstOrDefault() as RibbonPageGroup;
                            if(realGroup != null)
                            {
                                int index = realPage.Groups.IndexOf(realGroup);
                                realPage.Groups.RemoveAt(index);
                                realGroup.ItemLinks.Add(group.ItemLinks.First());
                                realPage.Groups.Insert(index, realGroup);
                            }
                            else
                            {
                                realPage.Groups.Add(group);
                            }
                        }

                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {

                    foreach (object element in e.OldItems)
                    {
                        if (element is RibbonPageCategory)
                        {
                            RibbonPageCategory category = (RibbonPageCategory)element;
                            if (category.Pages.Count == 0) return;

                            RibbonPageCategory realCategory = regionTarget.Items.Where(item =>
                            {
                                if (item is RibbonPageCategory && ((RibbonPageCategory)item).Caption == category.Caption)
                                    return true;
                                return false;
                            }).FirstOrDefault() as RibbonPageCategory;
                            if (realCategory != null)
                                regionTarget.Items.Remove(realCategory);
                        }
                        else if (element is BarButtonItem)
                        {
                            BarButtonItem button = (BarButtonItem)element;

                            RibbonDefaultPageCategory realCategory = regionTarget.Items.Where(item => item is RibbonDefaultPageCategory).First() as RibbonDefaultPageCategory;
                            RibbonPage realPage = realCategory.Pages.Where(item =>
                            {
                                if (item.Caption.ToString() == UIConstants.MainCommandPageName)
                                    return true;
                                return false;
                            }).FirstOrDefault() as RibbonPage;
                            RibbonPageGroup realGroup = realPage.Groups.Where(item =>
                            {
                                if (item.Caption.ToString() == UIConstants.MainCommandGroupName)
                                    return true;
                                return false;
                            }).FirstOrDefault() as RibbonPageGroup;
                            int index = realPage.Groups.IndexOf(realGroup);
                            realPage.Groups.RemoveAt(index);
                            for(int i = 0; i < realGroup.ItemLinks.Count; i++)
                            {
                                if (realGroup.ItemLinks[i].ActualContent == button.Content)
                                {
                                    realGroup.ItemLinks.RemoveAt(i);
                                    break;
                                }
                            }
                            realPage.Groups.Insert(index, realGroup);
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
