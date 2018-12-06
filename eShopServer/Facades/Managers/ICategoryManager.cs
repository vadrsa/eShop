using EntityDTO.Products;
using ModelChangeTracking;
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
        Task<List<CategoryDTO>> GetAllAsync(CancellationToken token = new CancellationToken());
        
        Task<List<CategoryTreeItemDTO>> GetTreeAsync(CancellationToken token = new CancellationToken());
        Task<bool> SaveEditableListAsync(ITrackableCollection<CategoryDTO> trackableCollection, CancellationToken token = new CancellationToken());
    }
}
