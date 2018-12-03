using BusinessEntities.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facades.Managers
{
    public interface IImageManager
    {
        Task<Image> InsertBytesAsync(byte[] image, CancellationToken token = new CancellationToken());
        Task<Image> UpdateBytesAsync(byte[] bytes, string path, CancellationToken token = new CancellationToken());
        Task RemoveAsync(Image image, CancellationToken token = new CancellationToken());
    }
}
