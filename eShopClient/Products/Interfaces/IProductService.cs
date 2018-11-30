
using EntityDTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Products.Interfaces
{
    interface IProductService
    {
        List<ProductInfoDTO> GetAllProducts();
        ProductDetailDTO GetByID();
    }
}
