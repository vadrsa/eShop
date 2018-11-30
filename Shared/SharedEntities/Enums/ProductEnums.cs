using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SharedEntities.Enums
{
    public enum Availability
    {
        Unknown,
        [EnumMember(Value = "In Stock")]
        InStock,
        [EnumMember(Value = "Out of Stock")]
        OutOfStock
    }

    public enum ProductOrderBy
    {
        Name,
        Price
    }
}
