using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Usefull
{
    class Memento<T>
    {
        Dictionary<PropertyInfo, object> storedProperties =
                   new Dictionary<PropertyInfo, object>();

        public Memento(T originator)
        {
            var propertyInfos =
                typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                       .Where(p => p.CanRead && p.CanWrite);

            foreach (var property in propertyInfos)
            {
                this.storedProperties[property] = property.GetValue(originator, null);
            }
        }

        public void Restore(T originator)
        {
            foreach (var pair in this.storedProperties)
            {
                pair.Key.SetValue(originator, pair.Value, null);
            }
        }
    }


}
