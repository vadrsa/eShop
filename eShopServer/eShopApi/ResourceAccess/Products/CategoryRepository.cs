using BusinessEntities.Products;
using eShopApi;
using eShopApi.ResourceAccess.Base;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eShopApi.ResourceAccess.Products
{
    public class CategoryRepository : RepositoryBase<Category, CategoryRepository>
    {
        protected override Expression<Func<DBContext, ITable<Category>>> TableExpression => c => c.Categories;

    }
}
