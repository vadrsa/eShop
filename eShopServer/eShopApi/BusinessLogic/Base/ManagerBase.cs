using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BusinessEntities;
using eShopApi.ResourceAccess.Base;

namespace eShopApi.BusinessLogic.Base
{
    public abstract class ManagerBase<T, R>
        where T: IDEntityBase, new()
        where R: RepositoryBase<T, R>, new()
    {

        public R Repository{
            get
            {
                return typeof(R).GetProperty("Instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy).GetValue(null, null) as R;
            }
        }

        public T Insert(T obj)
        {
            return Repository.Insert(obj);
        }

        public async Task<T> InsertAsync(T obj){
            return await Repository.InsertAsync(obj);
        }

        public async Task<long> BulkInsertAsync(List<T> list)
        {
            return await Repository.BulkInsertAsync(list);
        }

        public void UpdateAsync(T obj){
            Repository.UpdateAsync(obj);
        }

        public virtual async Task<T> SelectByKeyAsync(int key){
            return await Repository.SelectByKeyAsync(key);
        }

        public virtual async Task<List<T>> SelectAllAsync()
        {
            return await Repository.SelectAllAsync();            
        }

        public virtual bool Remove(T obj)
        {
            try
            {
                Repository.DeleteAsync(obj);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async virtual Task<bool> Remove(int id)
        {
            return Remove(await SelectByKeyAsync(id));
        }


        protected void ExecuteTransaction(Action action, Action OnRollback = null)
        {

            using(DBContext context = new DBContext())
            {
                context.
                context.BeginTransaction();
                try
                {
                    action();
                }
                catch
                {
                    context.RollbackTransaction();
                    OnRollback?.Invoke();

                    return;
                }
                context.CommitTransaction();
            }
        }

        protected G ExecuteTransactionAsync<G>(Func<G> func, Action OnRollback = null)
        {
            using (DBContext context = new DBContext())
            {
                context.BeginTransaction();
                G res  = default(G); 
                try
                {
                    res = func();
                }
                catch
                {
                    context.RollbackTransaction();
                    OnRollback?.Invoke();
                    return default(G);
                }
                context.CommitTransaction();
                return res;
            }
        }

        protected async Task<G> ExecuteTransactionAsync<G>(Func<Task<G>> func, Action OnRollback = null)
        {
            using (DBContext context = new DBContext())
            {
                context.BeginTransaction();
                G res = default(G);
                try
                {
                    res = await func();
                }
                catch(Exception e)
                {
                    context.RollbackTransaction();
                    OnRollback?.Invoke();
                    throw e;
                }
                context.CommitTransaction();
                return res;
            }
        }

    }
}