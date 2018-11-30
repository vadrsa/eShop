using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eShopApi.ResourceAccess.Global
{
    public class AssetRepository
    {
        public AssetRepository()
        {
            _workingDirectory = Path.Combine(
                          Directory.GetCurrentDirectory(),
                          "wwwroot");
        }

        private string _assetPath = "assets";
        private string _workingDirectory;

        private string AssetPath { get
            {
                return Path.Combine(_workingDirectory, _assetPath);
            }
        }

        private string GetFullPath(string relativePath, string fileName)
        {
            string path = Path.Combine(AssetPath, relativePath);
            Directory.CreateDirectory(path);
            string fileFullPath = path + "/" + fileName;
            return fileFullPath;
        }

        private string GetRelativePath(string relativePath, string fileName)
        {
            string path = Path.Combine(_assetPath, relativePath);
            Directory.CreateDirectory(path);
            string res = path + "/" + fileName;
            return res;
        }

        public async Task<string> InsertAsync(byte[] bytes, string relativePath, string fileName, CancellationToken token = new CancellationToken())
        {
            string fileFullPath = GetFullPath(relativePath, fileName);
            if (File.Exists(fileFullPath))
                throw new ArgumentException("File " + fileFullPath + " already exists.");
            await File.WriteAllBytesAsync(fileFullPath, bytes, token);
            return GetRelativePath(relativePath, fileName);
        }

        public async Task<string> UpdateAsync(byte[] bytes, string relativePath, string fileName, CancellationToken token = new CancellationToken())
        {
            string fileFullPath = GetFullPath(relativePath, fileName);
            if (!File.Exists(fileFullPath))
                throw new ArgumentException("File " + fileFullPath + " doesnt exist.");
            File.Delete(fileFullPath);
            await File.WriteAllBytesAsync(fileFullPath, bytes, token);
            return GetRelativePath(relativePath, fileName);
        }

        public async void UpdateAsync(byte[] bytes, string filePath, CancellationToken token = new CancellationToken())
        {
            string fileFullPath = Path.Combine(AssetPath, filePath);
            if (!File.Exists(fileFullPath))
                throw new ArgumentException("File " + fileFullPath + " doesnt exist.");
            File.Delete(fileFullPath);
            await File.WriteAllBytesAsync(fileFullPath, bytes, token);
        }

        public void Delete(string relativePath, string fileName)
        {
            string fileFullPath = GetFullPath(relativePath, fileName);
            if (File.Exists(fileFullPath))
                File.Delete(fileFullPath);
        }
    }
}
