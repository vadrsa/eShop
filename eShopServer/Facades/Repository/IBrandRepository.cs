using BusinessEntities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facades.Repository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        IBrandRepository LoadWith(Expression<Func<Brand, object>> f);
    }
}
