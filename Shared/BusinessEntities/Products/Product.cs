using BusinessEntities.Global;
using LinqToDB.Mapping;
using Newtonsoft.Json;
using SharedEntities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessEntities.Products
{
    [Table("Products")]
    public class Product : IDEntityBase
    {

        [Column]
        public EntityStatus Status { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public int CategoryID { get; set; }

        [Association(ThisKey = nameof(CategoryID), OtherKey = "ID")]
        public Category Category { get; set; }

        [Column]
        public int BrandID { get; set; }

        [Association(ThisKey = nameof(BrandID), OtherKey = "ID")]
        public Brand Brand { get; set; }

        [Column]
        public int Price { get; set; }

        [Column]
        public string ProductCode { get; set; }

        [Column]
        public Availability Availability{ get; set; }

        [Column]
        public string Description{ get; set; }

        [Column]
        public int ImageID { get; set; }

        [Association(ThisKey = nameof(ImageID), OtherKey = "ID")]
        public Image Image { get; set; }
    }
    
    
}
