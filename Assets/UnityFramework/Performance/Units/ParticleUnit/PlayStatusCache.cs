using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Performance
{
    public class PlayStatusCache : IStatusCache
    {
        private float _simulationSpeed;
        private bool _playOnAwake;
        private ParticleSystem _targetSystem;
        public ParticleSystem TargetSystem
        {
            get => _targetSystem;
            set
            {
                if (value != null)
                {
                    Cached = false;
                }
                _targetSystem = value;
            }
        }

        public bool Cached { get; private set; }

        public void Cache()
        {
            _simulationSpeed = TargetSystem.main.simulationSpeed;
            _playOnAwake = TargetSystem.main.playOnAwake;
            Cached = true;
        }

        public void Restore()
        {
            if (Cached)
            {
                var main = TargetSystem.main;
                main.simulationSpeed = _simulationSpeed;
                main.playOnAwake = _playOnAwake;
            }
        }
    }
}
