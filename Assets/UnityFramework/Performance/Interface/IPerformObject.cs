using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Performance
{
    public interface IPerformObject
    {
        public GameObject gameObject { get; }
        public Transform transform { get; }
        public IPerformanceUnit PerformanceUnit { get; }
    }
}
