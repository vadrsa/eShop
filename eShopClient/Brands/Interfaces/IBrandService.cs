
using EntityDTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Brands.Interfaces
{
    interface IBrandService
    {
        List<BrandInfoDTO> GetAllProducts();
        BrandDetailDTO GetByID();
    }
}
