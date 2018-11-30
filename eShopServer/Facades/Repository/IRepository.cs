using System;
using System.Collections.Generic;
using System.Linq;
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
        T FindByID(int ID);
        Task<T> FindByIDAsync(int ID, CancellationToken token = new CancellationToken());
        List<T> Find(Func<T, bool> predicate);
        Task<List<T>> FindAsync(Func<T, bool> predicate, CancellationToken token = new CancellationToken());
        void Remove(T obj);
        void RemoveAsync(T obj, CancellationToken token = new CancellationToken());
        void Update(T obj);
        void UpdateAsync(T obj, CancellationToken token = new CancellationToken());
    }
}
