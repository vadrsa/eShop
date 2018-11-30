using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDTO.Products
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
    }
}
