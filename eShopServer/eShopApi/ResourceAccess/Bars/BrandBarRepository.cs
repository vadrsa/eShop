using BusinessEntities.Bars;
using eShopApi;
using eShopApi.ResourceAccess.Base;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eShopApi.ResourceAccess.Bars
{
    public class BrandBarRepository : RepositoryBase<BrandBarItem, BrandBarRepository>
    {
        protected override Expression<Func<DBContext, ITable<BrandBarItem>>> TableExpression => c => c.BrandBar;
    }
}
