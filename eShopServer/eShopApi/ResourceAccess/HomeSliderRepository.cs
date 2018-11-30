using BusinessEntities;
using eShopApi;
using eShopApi.ResourceAccess.Base;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eShopApi.ResourceAccess
{
    public class HomeSliderRepository : RepositoryBase<HomeSliderItem, HomeSliderRepository>
    {
        protected override Expression<Func<DBContext, ITable<HomeSliderItem>>> TableExpression => c => c.HomeSlider;
    }
}
