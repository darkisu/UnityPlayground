using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Timer
{
    public class Timer : TimerBase
    {
        private uint _internalRepeatCount = 0;
        /// <summary>
        /// How many times should this timer trigger events
        /// Zero is unlimited
        /// </summary>
        public uint RepeatingCount { get; set; }
        private uint _executedCount;
        public event Action OnRepeatEnds;

        private uint _internalInterval = 1;
        /// <summary>
        /// Trigger interval in milliseconds with minimum 1.
        /// </summary>
        public uint Interval
        {
            get => _internalInterval;
            set
            {
                _internalInterval = Math.Max(value, 1);
            }
        }
        private uint _elapsedInterval;
        public event Action OnTrigger;

        public override void OnUpdate(float deltaTime)
        {
            uint deltaTimeInMilli = (uint)(deltaTime * 1000);
            _elapsedInterval += deltaTimeInMilli;
            while(ShouldTrigger() && CanRepeat())
            {
                OnTrigger?.Invoke();
                _elapsedInterval -= Interval;
                _executedCount++;
            }

            if (!CanRepeat())
            {
                OnRepeatEnds?.Invoke();
                Stop();
            }
        }

        private bool CanRepeat()
        {
            return RepeatingCount == 0 || _executedCount < RepeatingCount;
        }

        private bool ShouldTrigger()
        {
            return _elapsedInterval >= Interval;
        }

        public override void Reset()
        {
            _elapsedInterval = 0;
            _executedCount = 0;
        }
    }
}
