using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    public static class IListExtension
    {
        public static void ForEachFromBack<T>(this IList<T> list, Func<T, bool> shouldKeepInList)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (!shouldKeepInList(list[i]))
                {
                    list.RemoveAt(i);
                }
            }
        }
    }
}
