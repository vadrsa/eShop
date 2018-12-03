using AutoMapper;
using BusinessEntities.Products;
using EntityDTO.Products;
using Facades.Managers;
using Facades.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Transactions;

namespace Managers.Implementation
{
    public class BrandManager : IBrandManager
    {
        IImageManager imageManager;

        private IServiceProvider serviceProvider;

        public BrandManager(IServiceProvider provider, IImageManager iManager)
        {
            imageManager = iManager;

            serviceProvider = provider;
        }

        public async Task<List<BrandInfoDTO>> GetAllAsync(CancellationToken token = default(CancellationToken))
        {
            using (var scope = serviceProvider.CreateScope())
            {
                return Mapper.Map<List<BrandInfoDTO>>(
                     await scope.ServiceProvider.GetService<IBrandRepository>().GetAllAsync(token));
            }
        }

        public async Task<BrandDetailDTO> FindByIDAsync(int id, CancellationToken token = default(CancellationToken))
        {
            using (var scope = serviceProvider.CreateScope())
            {
                return Mapper.Map<BrandDetailDTO>(
                     await scope.ServiceProvider.GetService<IBrandRepository>()
                     .LoadWith(b => b.Image)
                     .FindByIDAsync(id, token));
            }
        }

        [Transaction(IsolationLevel.ReadUncommitted)]
        public async Task<BrandDetailDTO> InsertAsync(BrandDetailDTO obj, CancellationToken token = default(CancellationToken))
        {
            if (obj.ImageBytes == null || obj.ImageBytes.Length == 0)
                throw new ArgumentException("Image cannot be empty.");
            Brand toInsert = Mapper.Map<Brand>(obj);
            toInsert.ImageID = (await imageManager.InsertBytesAsync(obj.ImageBytes)).ID;
            Brand inserted = await serviceProvider.GetService<IBrandRepository>().InsertAsync(toInsert);
            return await FindByIDAsync(inserted.ID);
        }

        [Transaction(IsolationLevel.ReadUncommitted)]
        public async Task RemoveAsync(BrandInfoDTO obj, CancellationToken token = default(CancellationToken))
        {
            Brand old = await (serviceProvider.GetService<IBrandRepository>()
                .LoadWith(p => p.Image)
                .FindByIDAsync(obj.ID));
            await serviceProvider.GetService<IBrandRepository>().RemoveAsync(Mapper.Map<Brand>(obj), token);
            await imageManager.RemoveAsync(old.Image);

        }

        [Transaction(IsolationLevel.ReadUncommitted)]
        public async Task<BrandDetailDTO> UpdateAsync(BrandDetailDTO obj, CancellationToken token = default(CancellationToken))
        {
            Brand old = (await serviceProvider.GetService<IBrandRepository>()
                .LoadWith(p => p.Image)
                .FindByIDAsync(obj.ID));
            Brand toUpd = Mapper.Map<Brand>(obj);
            if (obj.ImageBytes != null && obj.ImageBytes.Length != 0)
                toUpd.ImageID = (await imageManager.UpdateBytesAsync(obj.ImageBytes, old.Image.Path, token)).ID;
            else
            {
                toUpd.ImageID = old.ImageID;
            }
            await serviceProvider.GetService<IBrandRepository>().UpdateAsync(toUpd, token);
            return await FindByIDAsync(toUpd.ID);
        }
    }
}
