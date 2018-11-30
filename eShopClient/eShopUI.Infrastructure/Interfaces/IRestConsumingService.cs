using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eShopUI.Infrastructure.Interfaces
{
    public interface IRestInfoConsumingService<TInfo>
    {
        Task<List<TInfo>> Get(CancellationToken token = new CancellationToken());
        Task<bool> Delete(TInfo obj, CancellationToken token = new CancellationToken());

    }

    public interface IRestDetailConsumingService<TDetail>
    {
        Task<TDetail> GetByID(int id, CancellationToken token = new CancellationToken());
        Task<TDetail> Add(TDetail details, CancellationToken token = new CancellationToken());
        Task<bool> Update(TDetail details, CancellationToken token = new CancellationToken());

    }
}
