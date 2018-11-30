using BusinessEntities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryFacade.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
