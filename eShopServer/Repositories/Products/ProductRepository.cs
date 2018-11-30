using BusinessEntities.Products;
using LinqToDB;
using Repositories.Extensions;
using RepositoryFacade.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAll()
        {
            using(DBContext context = new DBContext())
            {
                return context.Products.ToList();
            }
        }

        public Task<List<Product>> GetAllAsync(CancellationToken token = new CancellationToken())
        {
            using (DBContext context = new DBContext())
            {
                return context.Products.ToListAsync(token);
            }
        }

        public List<Product> Find(Func<Product, bool> predicate)
        {
            using (DBContext context = new DBContext())
            {
                return context.Products.Where(predicate).ToList();
            }
        }

        public Task<List<Product>> FindAsync(Func<Product, bool> predicate, CancellationToken token = default(CancellationToken))
        {

            using (DBContext context = new DBContext())
            {
                return context.Products.Where(predicate).ToListAsync(token);
            }
        }

        public Product FindByID(int ID)
        {
            using (DBContext context = new DBContext())
            {
                return Find(p => p.ID == ID).FirstOrDefault();
            }
        }

        public async Task<Product> FindByIDAsync(int ID, CancellationToken token = default(CancellationToken))
        {
            using (DBContext context = new DBContext())
            {
                return (await FindAsync(p => p.ID == ID)).FirstOrDefault();
            }
        }

        public Product Insert(Product obj)
        {
            using (DBContext context = new DBContext())
            {
                obj.ID = context.InsertWithInt32Identity(obj);
                return obj;
            }
        }

        public async Task<Product> InsertAsync(Product obj, CancellationToken token = default(CancellationToken))
        {
            using (DBContext context = new DBContext())
            {
                obj.ID = await context.InsertWithInt32IdentityAsync(obj);
                return obj;
            }
        }

        public void Remove(Product obj)
        {
            using (DBContext context = new DBContext())
            {
                context.Delete(obj);
            }
        }

        public async void RemoveAsync(Product obj, CancellationToken token = default(CancellationToken))
        {
            using (DBContext context = new DBContext())
            {
                await context.DeleteAsync(obj);
            }
        }

        public void Update(Product obj)
        {
            using (DBContext context = new DBContext())
            {
                context.Update(obj);
            }
        }

        public void UpdateAsync(Product obj, CancellationToken token = default(CancellationToken))
        {
            using (DBContext context = new DBContext())
            {
                context.UpdateAsync(obj);
            }
        }
    }
}
