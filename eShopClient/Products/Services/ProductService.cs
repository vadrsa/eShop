using Modules.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityDTO.Products;
using eShopUI.Infrastructure.Api;
using Flurl;
using Flurl.Http;

namespace Modules.Products.Services
{
    class ProductInfoService : RestInfoConsumingService<ProductInfoDTO>
    {
        public ProductInfoService() : base("products")
        {
        }
    }

    class ProductDetailService : RestDetailConsumingService<ProductDetailDTO>
    {
        public ProductDetailService() : base("products")
        {
        }
    }
    
}
