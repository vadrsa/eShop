using DevExpress.Utils.Design;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DXInfrastructure.Imaging
{
    public static class AppImageHelper
    {
        public static ImageSource GetImageSource(string name, ImageSize size)
        {
            try
            {
                return DXImageHelper.GetImageSource(name, size);
            }
            catch
            {

            }
            Bitmap bitmap = Properties.Resources.ResourceManager.GetObject(name + ((size == ImageSize.Size16x16) ? "_16x16" : "_32x32")) as Bitmap;
            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }

        
    }
}
