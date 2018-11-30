using System;
using System.Collections.Generic;
using System.Text;

namespace EntityDTO.Products
{
    public class CategoryTreeItemDTO
    {
        public CategoryTreeItemDTO() { }

        public CategoryTreeItemDTO(int id, string name, int productCount = 0)
        {
            ID = id;
            Name = name;
            ProductCount = productCount;
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public int ProductCount { get; set; }

        public IEnumerable<CategoryTreeItemDTO> Children { get; set; }

    }
}
