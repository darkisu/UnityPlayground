using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Darkisu
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> target)
        {
            return target ?? Enumerable.Empty<T>();
        }
    }
}
