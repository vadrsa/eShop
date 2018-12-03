using EntityDTO.Products;
using eShopUI.Infrastructure.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Products.Services
{
    class BrandInfoService : RestInfoConsumingService<BrandInfoDTO>
    {
        public BrandInfoService() : base("brands")
        {
        }
    }

    class BrandDetailService : RestDetailConsumingService<BrandDetailDTO>
    {
        public BrandDetailService() : base("brands")
        {
        }
    }
}
