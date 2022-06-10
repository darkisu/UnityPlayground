using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Performance.Extensions
{
    public static class IPerformanceUnitExtension
    {
        public static void SetTime(this IPerformanceUnit unit, float time)
        {
            unit.Time = time;
        }
    }
}
