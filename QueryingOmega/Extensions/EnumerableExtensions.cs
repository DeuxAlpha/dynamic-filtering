using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QueryingOmega.Extensions
{
    internal static class EnumerableExtensions
    {
        public static bool Empty<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }
    }
}