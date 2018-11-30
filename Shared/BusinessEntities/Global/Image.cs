using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities.Global
{
    [Table("Images")]
    public class Image : IDEntityBase
    {
        [Column]
        public string Path { get; set; }
    }
}
