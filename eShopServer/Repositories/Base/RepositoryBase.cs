using BusinessEntities;
using Facades.Repository;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Base
{
    public abstract class RepositoryBase<T, R> : IRepository<T>
        where T : IDEntityBase
        where R : class
    {

        List<Expression<Func<T, object>>> LoadWithList = new List<Expression<Func<T, object>>>();
        int _offset;
        int _limit;
        Func<T, object> _orderBy;
        bool _ascending;

        public R LoadWith(Expression<Func<T, object>> exp)
        {
            LoadWithList.Add(exp);
            return this as R;
        }

        public R Offset(int offset)
        {
            _offset = offset;
            return this as R;
        }

        public R Limit(int limit)
        {
            _limit = limit;
            return this as R;
        }

        private Func<T, object> GetOrderByExpression(string sortColumn)
        {
            Func<T, object> orderByExpr = null;
            if (!String.IsNullOrEmpty(sortColumn))
            {
                Type sponsorResultType = typeof(T);

                if (sponsorResultType.GetProperties().Any(prop => prop.Name == sortColumn))
                {
                    System.Reflection.PropertyInfo pinfo = sponsorResultType.GetProperty(sortColumn);
                    orderByExpr = (data => pinfo.GetValue(data, null));
                }
            }
            return orderByExpr;
        }

        public R OrderBy(string orderBy, bool ascending = true)
        {
            _ascending = ascending;
            _orderBy = GetOrderByExpression(orderBy);
            return this as R;
        }

        private ITable<T> GetTable(DBContext context)
        {
            ITable<T> table = TableExpression.Compile()(context);
            foreach (var exp in LoadWithList)
            {
                table = table.LoadWith(exp);
            }
            return table;
        }

        protected long BulkInsert(List<T> list)
        {
            using (DBContext context = new DBContext())
            {
                return context.BulkCopy(list).RowsCopied;
            }
        }

        protected IEnumerable<T> ExecuteSelect(Func<ITable<T>, IEnumerable<T>> func, DBContext context)
        {
            IEnumerable<T> res = func(GetTable(context));


            if (_orderBy != null)
            {
                if (_ascending)
                    res = res.OrderBy(_orderBy.Clone() as Func<T, object>);
                else
                    res = res.OrderByDescending(_orderBy.Clone() as Func<T, object>);
            }

            if (_offset != 0)
                res = res.Skip(_offset);
            _offset = 0;

            if (_limit != 0)
                res = res.Take(_limit);
            _limit = 0;

            _orderBy = null;
            LoadWithList.Clear();
            return res;
        }

        protected async Task<IEnumerable<T>> ExecuteSelectAsync(Func<ITable<T>, IEnumerable<T>> func, DBContext context)
        {
            return await Task.Run(() => ExecuteSelect(func, context));
        }

        protected abstract Expression<Func<DBContext, ITable<T>>> TableExpression { get; }
       

        public virtual T Insert(T obj)
        {
            using (DBContext context = new DBContext())
            {
                obj.ID = context.InsertWithInt32Identity(obj);
                return obj;
            }
        }

        public virtual async Task<T> InsertAsync(T obj, CancellationToken token = new CancellationToken())
        {
            using (DBContext context = new DBContext())
            {
                obj.ID = await context.InsertWithInt32IdentityAsync(obj);
                return obj;
            }
        }

        public virtual int Update(T obj)
        {
            using (DBContext context = new DBContext())
            {
                return context.Update(obj);
            }
        }

        public virtual async Task<int> UpdateAsync(T obj, CancellationToken token = new CancellationToken())
        {
            using (DBContext context = new DBContext())
            {
                return await context.UpdateAsync(obj);
            }
        }

        public virtual int Remove(T obj)
        {
            using (DBContext context = new DBContext())
            {
                return context.Delete(obj);
            }
        }

        public virtual async Task<int> RemoveAsync(T obj, CancellationToken token = new CancellationToken())
        {
            using (DBContext context = new DBContext())
            {
                return await context.DeleteAsync(obj);
            }
        }

        public virtual T FindByID(int key)
        {
            using (DBContext context = new DBContext())
            {
                return ExecuteSelect(t => t.Where(o => o.ID == key), context).Single();
            }
        }

        public virtual async Task<T> FindByIDAsync(int key, CancellationToken token = new CancellationToken())
        {
            using (DBContext context = new DBContext())
            {
                return (await ExecuteSelectAsync(t => t.Where(o => o.ID == key), context)).Single();
            }
        }

        public virtual List<T> GetAll( )
        {
            using (DBContext context = new DBContext())
            {
                return ExecuteSelect(t => t, context).ToList();
            }
        }

        public virtual async Task<List<T>> GetAllAsync( CancellationToken token = new CancellationToken())
        {
            using (DBContext context = new DBContext())
            {
                return (await ExecuteSelectAsync(t => t, context)).ToList();
            }
        }

        public virtual List<T> Find(Func<T, bool> where)
        {
            using (DBContext context = new DBContext())
            {
                return ExecuteSelect(t => t.Where(where), context).ToList();
            }
        }

        public virtual async Task<List<T>> FindAsync(Func<T, bool> where, CancellationToken token = new CancellationToken())
        {
            using (DBContext context = new DBContext())
            {
                return (await ExecuteSelectAsync(t => t.Where(where), context)).ToList();
            }
        }

    }
}
