using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Error
    {
        public int StatusCode { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
