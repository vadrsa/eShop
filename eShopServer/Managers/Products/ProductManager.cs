using BusinessEntities.Products;
using EntityDTO.Products;
using Facades.Managers;
using Facades.Repository;
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


        public ProductManager(ServiceProvider provider)
        {

        }

        public List<ProductDetailDTO> Find(Func<ProductDetailDTO, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDetailDTO>> FindAsync(Func<ProductDetailDTO, bool> predicate, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public ProductDetailDTO FindByID(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDetailDTO> FindByIDAsync(int ID, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public List<ProductInfoDTO> GetAll()
        {
            throw new NotImplementedException();

        }

        public Task<List<ProductInfoDTO>> GetAllAsync(CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public ProductDetailDTO Insert(ProductDetailDTO obj)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDetailDTO> InsertAsync(ProductDetailDTO obj, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Remove(ProductDetailDTO obj)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(ProductDetailDTO obj, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public void Update(ProductDetailDTO obj)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(ProductDetailDTO obj, CancellationToken token = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
