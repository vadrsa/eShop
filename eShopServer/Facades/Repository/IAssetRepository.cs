using BusinessEntities.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facades.Repository
{
    public interface IAssetRepository
    {
        string Insert(Asset asset);
        Task<string> InsertAsync(Asset asset, CancellationToken token = new CancellationToken());
        string Update(Asset asset);
        Task<string> UpdateAsync(Asset asset, CancellationToken token = new CancellationToken());
        void Remove(string path);
        Asset GetByPath(string path);
        Task<Asset> GetByPathAsync(string path, CancellationToken token = new CancellationToken());
    }
}
