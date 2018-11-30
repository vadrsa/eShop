using BusinessEntities.Products;
using eShopApi.ResourceAccess.Products;
using eShopApi.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedEntities.Enums;
using eShopApi.BusinessLogic.Global;

namespace eShopApi.BusinessLogic.Products
{
    public class ProductManager : ManagerBase<Product, ProductRepository>
    {
        public ProductManager(ImageManager imageManager)
        {
            ImageManager = imageManager;
        }

        private ImageManager ImageManager { get; set; }


        public async override Task<Product> SelectByKeyAsync(int id)
        {
            return await Repository
                .LoadWith(p => p.Category)
                .LoadWith(p => p.Brand)
                .LoadWith(p => p.Image)
                .LoadWith(p => p.ImageBig)
                .SelectByKeyAsync(id);
        }

        public async override Task<List<Product>> SelectAllAsync() { 
            return await Repository
                .LoadWith(p => p.Category)
                .LoadWith(p => p.Brand)
                .SelectWhereAsync(p => 
                p.Status == EntityStatus.Active);
        }

        public async Task<Product> InsertWithImageAsync(Product product, byte[] image)
        {
            return await ExecuteTransactionAsync(async () =>
            {
                product.ImageID = (await ImageManager.InsertBytesAsync(image)).ID;
                return await InsertAsync(product);
            });
        }
    }
}
