using eShopApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApi.Helpers
{
    public static class ImageHelper
    {

        private static Bitmap ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new Bitmap(ms);
                return image;
            }
        }

        private static string imagesFolder = "assets/images/";

        public static string GetImageExtension(byte[] bytes)
        {
            Bitmap bitmap = ToImage(bytes);
            string ext = null;
            if (bitmap.RawFormat.Equals(ImageFormat.Bmp)) ext = "bmp";
            else if (bitmap.RawFormat.Equals(ImageFormat.Jpeg)) ext = "jpg";
            else if (bitmap.RawFormat.Equals(ImageFormat.Png)) ext = "png";
            return ext;
        }

        public static string GetImagePath(byte[] image)
        {
            try
            {
                string filename = Guid.NewGuid().ToString();
                string ext = "";

                Bitmap bitmap = ToImage(image);
                if (bitmap.RawFormat.Equals(ImageFormat.Bmp)) ext = "bmp";
                else if (bitmap.RawFormat.Equals(ImageFormat.Jpeg)) ext = "jpg";
                else if (bitmap.RawFormat.Equals(ImageFormat.Png)) ext = "png";
                else throw new UnsupportedImageFormatException();
                
                string file = filename + "." + ext;

                var fullPathFolder = Path.Combine(
                          Directory.GetCurrentDirectory(),
                          "wwwroot", imagesFolder);
                if (Directory.Exists(fullPathFolder))
                {
                    File.WriteAllBytes(fullPathFolder + file, image);
                }
                return imagesFolder + file;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
