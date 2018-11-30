using BusinessEntities.Products;
using eShopApi.ResourceAccess.Bars;
using eShopApi.ResourceAccess.Products;
using eShopApi.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedEntities.Enums;

namespace eShopApi.BusinessLogic.Products
{
    public class BrandManager : ManagerBase<Brand, BrandRepository>
    {
        public async Task<IEnumerable<Brand>> GetBrandBarAsync()
        {
            return (await BrandBarRepository.Instance.LoadWith(b => b.Brand).SelectAllAsync()).Select(b => b.Brand);
        }

        internal async Task<IEnumerable<Product>> GetProductsAsync(int id, int limit, int start, ProductOrderBy orderBy = ProductOrderBy.Name, bool ascending = true)
        {
            ProductRepository repo = ProductRepository.Instance.Limit(limit);
            if (start > 0) repo = repo.Offset(start);
            return await repo.OrderBy(orderBy.ToString(), ascending).SelectWhereAsync(p => p.BrandID == id);
        }

        internal async Task<int> GetProductsCount(int id)
        {
            return await ProductRepository.Instance.SelectCountWhereAsync(p => p.BrandID == id);
        }
    }
}
