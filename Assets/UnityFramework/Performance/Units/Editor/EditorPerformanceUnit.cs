using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Darkisu.Performance.Extensions;

namespace Darkisu.Performance.Editor
{
    [EditorPerformanceUnit(typeof(IPerformanceUnit))]
    public class EditorPerformanceUnit
    {
        public IPerformanceUnit PerformanceUnit { get; private set; }
        public EditorPerformanceUnit(IPerformanceUnit targetUnit)
        {
            PerformanceUnit = targetUnit;
        }

        public virtual bool UpdateTo(float time)
        {
            PerformanceUnit?.SetTime(time);
            return PerformanceUnit.IsPlaying;
        }

        public virtual void OnPlay()
        {
            PerformanceUnit?.Play();
        }
        public virtual void OnStop()
        {
            PerformanceUnit?.Stop();
        }
    }
}
