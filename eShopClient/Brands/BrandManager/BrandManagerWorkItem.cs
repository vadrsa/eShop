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
using Modules.Brands.Constants;
using Modules.Brands.BrandManager.Views;
using DevExpress.Xpf.Editors;

namespace Modules.Products
{
    class BrandManagerWorkItem : ObjectManagerWorkItem<BrandsListView, BrandsDetailView, BrandInfoDTO, BrandDetailDTO, BrandInfoViewModel, BrandDetailViewModel>
    {
        public BrandManagerWorkItem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName
        {
            get
            {
                return UIConstants.BarndManagerWorkItemName;
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
