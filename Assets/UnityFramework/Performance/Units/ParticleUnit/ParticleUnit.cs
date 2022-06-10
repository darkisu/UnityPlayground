using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Performance
{
    [Serializable]
    public class ParticleUnit : IPerformanceUnit
    {
        #region Particle Settings
        private RandomSeedCache _randomSeedCache = new RandomSeedCache();
        private PlayStatusCache _playStatusCache = new PlayStatusCache();
        public ParticleUnit()
        {
        }

        public ParticleUnit(ParticleSystem target)
        {
            Target = target;
        }

        [SerializeField]
        private ParticleSystem _target;
        /// <summary>
        /// The target particle system of the unit to control
        /// </summary>
        public ParticleSystem Target
        {
            get => _target;
            set
            {
                if (value != _target)
                {
                    var originalTime = Time;
                    var originalPlaybackSpeed = IsValid ? PlaybackSpeed : 1;
                    var originalIsPlaying = IsPlaying;

                    if (originalIsPlaying)
                    {
                        Stop();
                    }
                    _playStatusCache.Restore();
                    _target = value;
                    _playStatusCache.Cache();
                    var mainModule = Target.main;
                    mainModule.playOnAwake = false;

                    if (!originalIsPlaying)
                    {
                        Stop();
                    }

                    Time = originalTime;
                    PlaybackSpeed = originalPlaybackSpeed;
                }
            }
        }
        [SerializeField]
        private ParticleSystemStopBehavior _stopBehavior = ParticleSystemStopBehavior.StopEmittingAndClear;
        public ParticleSystemStopBehavior StopBehavior
        {
            get => _stopBehavior;
            set => _stopBehavior = value;
        }

        public bool IsLooping
        {
            get => IsValid ? Target.main.loop : false;
            set 
            {
                if (IsValid)
                {
                    var mainModule = Target.main;
                    mainModule.loop = value;
                }
            }
        }
        #endregion

        #region IPerformUnit
        public float Time
        {
            get => IsValid ? Target.time : default; 
            set 
            {
                if (!IsPlaying)
                {
                    Play();
                }
                if (IsValid)
                {
                    var shouldStop = !IsLooping && value > Target.main.duration;
                    shouldStop = shouldStop || value < 0;

                    if (shouldStop)
                    {
                        Stop();
                        return;
                    }

                    var delta = value - Time;
                    var replay = delta < 0;
                    var simulateTarget = replay ? value : delta;

                    if (replay && StopBehavior == ParticleSystemStopBehavior.StopEmittingAndClear)
                    {
                        Stop();
                        _randomSeedCache.Restore();
                    }
                    
                    Target.Simulate(simulateTarget, true, replay);
                }
            }
        }

        public bool IsPlaying => IsValid && (Target.isPlaying || Target.isPaused);

        public float PlaybackSpeed
        {
            get => IsValid ? Target.main.simulationSpeed : default; 
            set
            {
                if (IsValid)
                {
                    var mainModule = Target.main;
                    if (_playStatusCache.TargetSystem != Target)
                    {
                        _playStatusCache.TargetSystem = Target;
                        _playStatusCache.Cache();
                    }
                    mainModule.simulationSpeed = value;
                }
                if (IsSelfUpdating)
                {
                    _playbackSpeedCache = value;
                }
            }
        }

        private float _playbackSpeedCache = 1;
        public bool IsSelfUpdating 
        { 
            get => PlaybackSpeed != 0; 
            set
            {
                if (value != IsSelfUpdating)
                {
                    if (value)
                    {
                        PlaybackSpeed = 0;
                    }
                    else
                    {
                        PlaybackSpeed = _playbackSpeedCache;
                    }
                }
            }
        }

        public void Play()
        {
            if (IsValid)
            {
                Target.Play();
                _randomSeedCache.TargetSystem = Target;
                _randomSeedCache.Cache();
            }
        }

        public void Stop()
        {
            if (IsValid)
            {
                Target.Stop(true, StopBehavior);
            }
        }
        #endregion

        public bool IsValid => Target != null;
    }
}
