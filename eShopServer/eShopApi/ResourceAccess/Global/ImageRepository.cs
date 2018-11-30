using BusinessEntities.Global;
using eShopApi.ResourceAccess.Base;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eShopApi.ResourceAccess.Global
{
    public class ImageRepository : RepositoryBase<Image, ImageRepository>
    {
        protected override Expression<Func<DBContext, ITable<Image>>> TableExpression => (d) => d.Images;
        
    }
}
