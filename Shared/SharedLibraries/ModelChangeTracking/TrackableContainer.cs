using ModelChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelChangeTracking
{
    public class TrackableContainer<T> : ITrackableCollection<T>
    {
        public TrackableContainer() { }
        public TrackableContainer(IEnumerable<T> changed, IEnumerable<T> newItems, IEnumerable<T> deleted)
        {
            Changed = changed;
            New = newItems;
            Deleted = deleted;
        }

        public IEnumerable<T> Changed { get; set; }

        public IEnumerable<T> New { get; set; }

        public IEnumerable<T> Deleted { get; set; }
    }
}
