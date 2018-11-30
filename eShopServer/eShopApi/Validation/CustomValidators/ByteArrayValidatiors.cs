using FluentValidation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApi.Validation.CustomValidators
{
    public static class ByteArrayValidatiors
    {
        public static IRuleBuilderOptions<T, byte[]> MustBeImage<T>(this IRuleBuilder<T, byte[]> ruleBuilder)
        {
            return ruleBuilder.Must(bytes => {
                try
                {
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        Bitmap bmp = new Bitmap(stream);
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}
