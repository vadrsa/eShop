using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXInfrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GridColumnAttribute : Attribute
    {
        public GridColumnAttribute(string header = null, int order = 0)
        {
            Header = header;
            Order = order;
        }

        public string Header { get; set; }

        public int Order { get; set; }

        public string FieldName { get; set; }
    }
}
