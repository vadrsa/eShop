
using SharedEntities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityDTO.Products
{
    public class ProductDetailDTO
    {
        public int ID { get; set; }

        public EntityStatus Status { get; set; }
        
        public string Name { get; set; }
        
        public int CategoryID { get; set; }

        public string Category { get; set; }
        
        public int BrandID { get; set; }

        public string Brand { get; set; }
        
        public int Price { get; set; }
        
        public string ProductCode { get; set; }
        
        public Availability Availability { get; set; }

        public string Description { get; set; }
        
        public string Image { get; set; }

        public byte[] ImageBytes { get; set; }
    }
}
