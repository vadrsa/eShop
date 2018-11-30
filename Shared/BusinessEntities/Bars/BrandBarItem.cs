using BusinessEntities.Products;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Bars
{
    [Table("BrandBar")]
    public class BrandBarItem : IDEntityBase
    {
        [Column]
        public int BrandID { get; set; }

        [Association(ThisKey = "BrandID", OtherKey ="ID")]
        public Brand Brand { get; set; }
    }
}
