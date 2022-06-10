using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    public static class ICollectionExtension
    {
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return !(collection != null && collection.Count > 0);
        }
    }
}
