using BusinessEntities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facades.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
