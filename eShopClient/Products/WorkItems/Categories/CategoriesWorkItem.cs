using eShopUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopUI.Infrastructure.Interfaces;
using Prism.Ioc;
using eShop.EntityViewModels;
using EntityDTO.Products;
using eShop.EntityViewModels.Products;
using Modules.Products.Constants;
using Modules.Products.WorkItems.BrandManager.Views;
using DevExpress.Xpf.Editors;
using Modules.Products.WorkItems.Categories.ViewModels;
using Modules.Products.WorkItems.Categories.Views;

namespace Modules.Products.WorkItems.Categories
{
    class CategoriesWorkItem : ContentWorkItem<CategoryDTO, CategoriesView, CategoryViewModel>
    {
        public CategoriesWorkItem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName
        {
            get
            {
                return UIConstants.CategoriesWorkItemName;
            }
        }

        protected override void AddAdditionalCommandGroups()
        {
            base.AddAdditionalCommandGroups();
            
        }

        public override void Run()
        {
            base.Run();
            
        }
    }
}
