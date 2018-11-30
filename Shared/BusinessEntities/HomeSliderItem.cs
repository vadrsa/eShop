using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    [Table("HomeSlider")]
    public class HomeSliderItem : IDEntityBase
    {
        [Column]
        public string Image { get; set; }

        [Column, Nullable]
        public string Description { get; set; }

    }
}
