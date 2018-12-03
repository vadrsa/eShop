using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Exceptions
{
    [Serializable]
    public class UnsupportedImageFormatException : Exception
    {
        public UnsupportedImageFormatException()
        {
        }
    }
}
