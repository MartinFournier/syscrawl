using System.Collections.Generic;

namespace syscrawl.Common.Extensions
{
    public static class CollectionExtensions
    {
        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            return new Queue<T>(enumerable);
        }
    }
}

