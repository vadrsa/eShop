using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repositories.Extensions
{
    internal static class AsyncExtensions
    {
        public static Task<List<T>> ToListAsync<T>(this IEnumerable<T> list, CancellationToken token = default(CancellationToken))
        {
            return Task.Run(() => {
                List<T> res = new List<T>();
                int i = 0;
                foreach(T item in list)
                {
                    if (i % 100 == 0) token.ThrowIfCancellationRequested(); 
                    res.Add(item);
                    i++;
                }
                return res;
            });
        }
    }
}
