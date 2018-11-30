using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Products
{
    [Table("Brands")]
    public class Brand : IDEntityBase
    {
        [Column]
        public string Name { get; set; }

        [Column]
        public string Image { get; set; }
    }
}
