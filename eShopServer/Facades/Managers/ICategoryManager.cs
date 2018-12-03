using EntityDTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facades.Managers
{
    public interface ICategoryManager
    {
        List<CategoryDTO> GetAll();
        Task<List<CategoryDTO>> GetAllAsync(CancellationToken token = new CancellationToken());

        List<CategoryTreeItemDTO> GetTree();
        Task<List<CategoryTreeItemDTO>> GetTreeAsync(CancellationToken token = new CancellationToken());
    }
}
