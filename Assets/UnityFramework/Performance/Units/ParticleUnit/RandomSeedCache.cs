using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Performance
{
    public class RandomSeedCache : IStatusCache
    {
        private ParticleSystem[] _particleSystems;
        private Dictionary<ParticleSystem, uint> _systemSeedCache = new Dictionary<ParticleSystem, uint>();
        public ParticleSystem RootSystem 
        {
            get => _particleSystems.IsNullOrEmpty() ? null : _particleSystems[0];
            set
            {
                if (value != RootSystem)
                {
                    _particleSystems = value.GetComponentsInChildren<ParticleSystem>();
                    _systemSeedCache.Clear();
                }
            }
        }

        public void Cache()
        {
            _systemSeedCache.Clear();
            foreach(var particleSystem in _particleSystems.EmptyIfNull())
            {
                _systemSeedCache[particleSystem] = particleSystem.randomSeed;
            }
        }

        public void Restore()
        { 
            foreach(var particleSystem in _particleSystems.EmptyIfNull())
            {
                if (_systemSeedCache.TryGetValue(particleSystem, out uint seed))
                {
                    particleSystem.randomSeed = seed;
                }
            }
        }
    }
}
