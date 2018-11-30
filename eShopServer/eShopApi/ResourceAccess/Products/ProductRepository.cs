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
    public class ProductRepository : RepositoryBase<Product, ProductRepository>
    {
        protected override Expression<Func<DBContext, ITable<Product>>> TableExpression => c => c.Products;
    }
}
