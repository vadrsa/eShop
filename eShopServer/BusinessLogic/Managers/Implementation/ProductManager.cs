using AutoMapper;
using BusinessEntities.Products;
using EntityDTO.Products;
using Facades.Managers;
using Facades.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Transactions;

namespace Managers.Implementation
{
    public class ProductManager : IProductManager
    {
        IImageManager imageManager;
        private IServiceProvider serviceProvider;

        public ProductManager(IServiceProvider provider, IImageManager iManager)
        {
            serviceProvider = provider;
            imageManager = iManager;
        }

        public async Task<ProductDetailDTO> FindByIDAsync(int ID, CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<ProductDetailDTO>(
                    await serviceProvider.GetService<IProductRepository>()
                                .LoadWith(p => p.Category)
                                .LoadWith(p => p.Brand)
                                .LoadWith(p => p.Image)
                                .FindByIDAsync(ID, token));
        }

        public async Task<List<ProductInfoDTO>> GetAllAsync(CancellationToken token = default(CancellationToken))
        {
            return Mapper.Map<List<ProductInfoDTO>>(await serviceProvider.GetService<IProductRepository>()
                                .LoadWith(p => p.Category)
                                .LoadWith(p => p.Brand)
                                .GetAllAsync(token));
        }
        
        [Transaction(IsolationLevel.ReadUncommitted)]
        public async Task<ProductDetailDTO> InsertAsync(ProductDetailDTO obj, CancellationToken token = default(CancellationToken))
        {
            if (obj.ImageBytes == null || obj.ImageBytes.Length == 0)
                throw new ArgumentException("Image cannot be empty.");
            Product toInsert = Mapper.Map<Product>(obj);
            toInsert.ImageID = (await imageManager.InsertBytesAsync(obj.ImageBytes)).ID;
            Product inserted = await serviceProvider.GetService<IProductRepository>().InsertAsync(toInsert);
            return await FindByIDAsync(inserted.ID);
        }
        
        [Transaction(IsolationLevel.ReadUncommitted)]
        public async Task RemoveAsync(ProductInfoDTO obj, CancellationToken token = default(CancellationToken))
        {
            Product old = await (serviceProvider.GetService<IProductRepository>()
                .LoadWith(p => p.Image)
                .FindByIDAsync(obj.ID));
            await serviceProvider.GetService<IProductRepository>().RemoveAsync(Mapper.Map<Product>(obj), token);
            await imageManager.RemoveAsync(old.Image);

        }

        [Transaction(IsolationLevel.ReadUncommitted)]
        public async Task<ProductDetailDTO> UpdateAsync(ProductDetailDTO obj, CancellationToken token = default(CancellationToken))
        {
            Product old = (await serviceProvider.GetService<IProductRepository>()
                .LoadWith(p => p.Image)
                .FindByIDAsync(obj.ID));
            Product toUpd = Mapper.Map<Product>(obj);
            if(obj.ImageBytes != null && obj.ImageBytes.Length != 0)
                toUpd.ImageID = (await imageManager.UpdateBytesAsync(obj.ImageBytes, old.Image.Path, token)).ID;
            else
            {
                toUpd.ImageID = old.ImageID;
            }
            await serviceProvider.GetService<IProductRepository>().UpdateAsync(toUpd, token);
            return await FindByIDAsync(toUpd.ID);
        }
    }
}
