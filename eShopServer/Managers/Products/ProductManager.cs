using BusinessEntities.Products;
using Facades.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Managers.Products
{
    public class ProductManager : IProductManager
    {
        public List<Product> Find(Func<Product, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> FindAsync(Func<Product, bool> predicate, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Product FindByID(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FindByIDAsync(int ID, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllAsync(CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Product Insert(Product obj)
        {
            throw new NotImplementedException();
        }

        public Task<Product> InsertAsync(Product obj, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Remove(Product obj)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(Product obj, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Update(Product obj)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(Product obj, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
