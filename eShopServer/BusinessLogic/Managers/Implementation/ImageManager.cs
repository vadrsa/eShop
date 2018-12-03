using BusinessEntities.Global;
using Facades.Managers;
using Facades.Repository;
using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Transactions;

namespace Managers.Implementation
{
    public class ImageManager: IImageManager
    {
        private IServiceProvider serviceProvider;

        public ImageManager(IServiceProvider provider)
        {
            serviceProvider = provider;
        }

        private string GetRandomFileName()
        {
            return Guid.NewGuid().ToString();
        }

        private string GetRelativeFilePath(byte[] image)
        {
            return Path.Combine("images", GetRandomFileName() + "." + ImageHelper.GetImageExtension(image));
        }

        [Transaction(System.Transactions.IsolationLevel.ReadUncommitted)]
        public async Task<Image> InsertBytesAsync(byte[] image, CancellationToken token = new CancellationToken())
        {
            string path = await serviceProvider.GetService<IAssetRepository>()
                .InsertAsync(new Asset() { Contents = image, RelativePath = GetRelativeFilePath(image) }, token);
            return await serviceProvider.GetService<IImageRepository>()
                .InsertAsync(new Image() { Path = path }, token);
        }
        public async Task<Image> UpdateBytesAsync(byte[] bytes, string path, CancellationToken token = new CancellationToken())
        {
            Image image = (await serviceProvider.GetService<IImageRepository>()
                .FindAsync(i => i.Path == path)).First();
            path = await serviceProvider.GetService<IAssetRepository>()
                .UpdateAsync(new Asset() { Contents = bytes, RelativePath = path }, token);
            return image;
        }
        
        public async Task RemoveAsync(Image image, CancellationToken token = default(CancellationToken))
        {
            Image original = await serviceProvider.GetService<IImageRepository>().FindByIDAsync(image.ID);
            int id = await serviceProvider.GetService<IImageRepository>().RemoveAsync(original, token);
            if(id != 0)
                serviceProvider.GetService<IAssetRepository>().Remove(original.Path);
        }
    }
}
