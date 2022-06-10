using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Performance
{
    public class TestPerformanceObject : MonoBehaviour, IPerformObject
    {
        [SerializeField]
        private ParticleUnit _unit;
        public IPerformanceUnit PerformanceUnit { get => _unit; }
    }
}
