using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDTO.Products
{
    public class BrandInfoDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class BrandDetailDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
