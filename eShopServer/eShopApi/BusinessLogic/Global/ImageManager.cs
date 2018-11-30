using BusinessEntities.Global;
using eShopApi.BusinessLogic.Base;
using eShopApi.Helpers;
using eShopApi.ResourceAccess.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApi.BusinessLogic.Global
{
    public class ImageManager : ManagerBase<Image, ImageRepository>
    {
        public ImageManager()
        {
            AssetRepository = new AssetRepository();
        }

        private AssetRepository AssetRepository { get; set; }

        private string GenerateRandomFileName()
        {
            return Guid.NewGuid().ToString();
        }

        public async Task<Image> InsertBytesAsync(byte[] image, string relativePath = "images")
        {
            string fileName = "";
            return await ExecuteTransactionAsync(async () => {
                string ext = ImageHelper.GetImageExtension(image);
                if (String.IsNullOrEmpty(ext)) throw new ArgumentException("Unkown image format.");
                fileName = GenerateRandomFileName() + "." + ext;
                Image res = new Image();
                res.Path = await AssetRepository.InsertAsync(image, relativePath, fileName);
                return await InsertAsync(res);
            }, () => {
                AssetRepository.Delete(relativePath, fileName);
            });
        }
    }
}
