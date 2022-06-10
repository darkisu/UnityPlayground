using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Darkisu.Performance.Editor
{
    [EditorPerformanceUnit(typeof(ParticleUnit))]
    public class EditorParticleUnit : EditorPerformanceUnit
    {
        private ParticleSystem _particleSystem => ((ParticleUnit)PerformanceUnit).Target;
        private RandomSeedCache _randomSeedCache;
        public EditorParticleUnit(IPerformanceUnit targetUnit) : base(targetUnit)
        {
        }

        public override bool UpdateTo(float time)
        {
            _randomSeedCache.Restore();
            _particleSystem.Simulate(time, true, true);
            return _particleSystem.main.duration >= time;
        }

        public override void OnPlay()
        {
            _randomSeedCache.Cache();
        }
        public override void OnStop()
        {
            _particleSystem.Simulate(0, true, true);
        }
    }
}
