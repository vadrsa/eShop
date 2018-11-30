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
    public class BrandRepository : RepositoryBase<Brand, BrandRepository>
    {
        protected override Expression<Func<DBContext, ITable<Brand>>> TableExpression => c => c.Brands;
    }
}
