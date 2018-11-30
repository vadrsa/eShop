using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BusinessEntities;
using LinqToDB;
using LinqToDB.Data;

namespace eShopApi.ResourceAccess.Base
{
    public abstract class RepositoryBase<T, R>
        where T: IDEntityBase
        where R:  RepositoryBase<T,R>, new()
    {

        List<Expression<Func<T, object>>> LoadWithList = new List<Expression<Func<T, object>>>();
        int _offset;
        int _limit;
        Func<T, object> _orderBy;
        bool _ascending;

        public R LoadWith(Expression<Func<T, object>> exp){
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

        private ITable<T> GetTable(DBContext context){
            ITable<T> table = TableExpression.Compile()(context);
            foreach(var exp in LoadWithList){
                table = table.LoadWith(exp);
            }
            return table;
        }

        public async Task<long> BulkInsertAsync(List<T> list)
        {
            using (DBContext context = new DBContext())
            {
                long res = await Task.Run(()=> context.BulkCopy(list).RowsCopied).ConfigureAwait(false);
                return res;
            }
        }

        private async Task<IEnumerable<T>> ExecuteSelect(Func<ITable<T>, IEnumerable<T>> func, DBContext context){
            IEnumerable<T> res = await Task.Run(() => func(GetTable(context))).ConfigureAwait(false);

            
            if (_orderBy != null)
            {
                if(_ascending)
                    res = await Task.Run(() => res.OrderBy(_orderBy.Clone() as Func<T, object>)).ConfigureAwait(false);
                else
                    res = await Task.Run(() => res.OrderByDescending(_orderBy.Clone() as Func<T, object>)).ConfigureAwait(false);
            }

            if (_offset != 0)
                res = await Task.Run(() => res.Skip(_offset)).ConfigureAwait(false);
            _offset = 0;

            if (_limit != 0)
                res = await Task.Run(() => res.Take(_limit)).ConfigureAwait(false);
            _limit = 0;

            _orderBy = null;
            LoadWithList.Clear();
            return res;
        }

        protected abstract Expression<Func<DBContext, ITable<T>>> TableExpression { get; }

        private static R instance;

        public static R Instance{
            get{
                return new R();
            }
        }


        public T Insert(T obj)
        {
            using (DBContext context = new DBContext())
            {
                obj.ID = context.InsertWithInt32Identity(obj);
                return obj;
            }
        }

        public async Task<T> InsertAsync(T obj){
            using(DBContext context =  new DBContext()){
                obj.ID = await context.InsertWithInt32IdentityAsync(obj);
                return obj;
            }
        }

        public async void UpdateAsync(T obj){
            using(DBContext context = new DBContext()){
                await context.UpdateAsync(obj);
            }
        }
        
        public async void DeleteAsync(T obj)
        {
            using (DBContext context = new DBContext())
            {
                await context.DeleteAsync(obj);
            }
        }

        public async Task<T> SelectByKeyAsync(int key){
            using(DBContext context = new DBContext()){
                return (await ExecuteSelect(t => t.Where(o => o.ID == key), context).ConfigureAwait(false)).Single();
            }
        }

        public async Task<List<T>> SelectAllAsync()
        {
            using(DBContext context = new DBContext()){
                return (await ExecuteSelect(t => t, context).ConfigureAwait(false)).ToList();
            }
        }

        public async Task<List<T>> SelectWhereAsync(Func<T,bool> where)
        {
            using (DBContext context = new DBContext())
            {
                return (await ExecuteSelect(t => t.Where(where), context).ConfigureAwait(false)).ToList();
            }
        }


        public async Task<int> SelectCountWhereAsync(Func<T, bool> where)
        {
            using (DBContext context = new DBContext())
            {
                return (await ExecuteSelect(t => t.Where(where), context).ConfigureAwait(false)).Count();
            }
        }

    }
}