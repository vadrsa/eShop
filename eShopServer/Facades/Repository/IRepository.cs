using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facades.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        Task<List<T>> GetAllAsync(CancellationToken token = new CancellationToken());
        T Insert(T obj);
        Task<T> InsertAsync(T obj, CancellationToken token = new CancellationToken());
        long InsertRange(List<T> obj);
        Task<long> InsertRangeAsync(List<T> obj, CancellationToken token = new CancellationToken());
        T FindByID(int ID);
        Task<T> FindByIDAsync(int ID, CancellationToken token = new CancellationToken());
        List<T> Find(Func<T, bool> predicate);
        Task<List<T>> FindAsync(Func<T, bool> predicate, CancellationToken token = new CancellationToken());
        int Remove(T obj);
        Task<int> RemoveAsync(T obj, CancellationToken token = new CancellationToken());
        int Update(T obj);
        Task<int> UpdateAsync(T obj, CancellationToken token = new CancellationToken());
    }
}
