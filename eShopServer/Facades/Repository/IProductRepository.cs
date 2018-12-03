using BusinessEntities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facades.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        IProductRepository LoadWith(Expression<Func<Product, object>> exp);

    }
}
