using BusinessEntities.Products;
using EntityDTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facades.Managers
{
    public interface IProductManager
    {
        Task<List<ProductInfoDTO>> GetAllAsync(CancellationToken token = new CancellationToken());
        Task<ProductDetailDTO> InsertAsync(ProductDetailDTO obj, CancellationToken token = new CancellationToken());
        Task<ProductDetailDTO> FindByIDAsync(int ID, CancellationToken token = new CancellationToken());
        Task RemoveAsync(ProductInfoDTO obj, CancellationToken token = new CancellationToken());
        Task<ProductDetailDTO> UpdateAsync(ProductDetailDTO obj, CancellationToken token = new CancellationToken());
    }
}
