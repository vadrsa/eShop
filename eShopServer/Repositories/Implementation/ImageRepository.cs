using BusinessEntities.Global;
using Facades.Repository;
using LinqToDB;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class ImageRepository : RepositoryBase<Image, IImageRepository>, IImageRepository
    {
        protected override Expression<Func<DBContext, ITable<Image>>> TableExpression => c => c.Images;
        
    }
}
