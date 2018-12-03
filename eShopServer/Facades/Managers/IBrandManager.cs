using EntityDTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facades.Managers
{
    public interface IBrandManager
    {
        Task<List<BrandInfoDTO>> GetAllAsync(CancellationToken token = new CancellationToken());
        Task<BrandDetailDTO> FindByIDAsync(int id, CancellationToken token = new CancellationToken());
        Task<BrandDetailDTO> InsertAsync(BrandDetailDTO obj, CancellationToken token = new CancellationToken());
        Task RemoveAsync(BrandInfoDTO obj, CancellationToken token = new CancellationToken());
        Task<BrandDetailDTO> UpdateAsync(BrandDetailDTO obj, CancellationToken token = new CancellationToken());
    }
}
