using eShopUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopUI.Infrastructure.Interfaces;
using Prism.Ioc;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using eShop.EntityViewModels;
using Modules.Products.Constants;
using EntityDTO.Products;
using eShop.EntityViewModels.Products;
using Modules.Products.ProductManager.Views;

namespace Modules.Products.ProductManager
{
    class ProductManagerWorkItem : ObjectManagerWorkItem<ProductsListView, ProductsDetailView, ProductInfoDTO, ProductDetailDTO, ProductInfoViewModel, ProductDetailViewModel>
    {
        public ProductManagerWorkItem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName
        {
            get
            {
                return UIConstants.ProductManagerWorkItemName;
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
