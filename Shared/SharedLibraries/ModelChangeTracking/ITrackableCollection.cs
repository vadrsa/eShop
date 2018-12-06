using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelChangeTracking
{
    public interface ITrackableCollection<T>
    {
        IEnumerable<T> Changed { get; }
        IEnumerable<T> New { get; }
        IEnumerable<T> Deleted { get; }
    }
}
