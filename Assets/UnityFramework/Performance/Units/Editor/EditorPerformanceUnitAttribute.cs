using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Performance.Editor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EditorPerformanceUnitAttribute : Attribute
    {
        public bool IsValid => typeof(IPerformanceUnit).IsAssignableFrom(RuntimePerformanceUnitType) || RuntimePerformanceUnitType == typeof(IPerformanceUnit);
        public Type RuntimePerformanceUnitType { get; private set; }
        public EditorPerformanceUnitAttribute(Type runtimeUnitType)
        {
            RuntimePerformanceUnitType = runtimeUnitType;
        }
    }
}
