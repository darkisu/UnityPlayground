using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Timer
{
    /// <summary>
    /// A timer updater that updates all timers in the scene, to have one in DontDestroyOnLoad is recommended
    /// </summary>
    public class TimerUpdater : SceneSingleton<TimerUpdater>
    {
        private HashSet<TimerBase> _timers = new HashSet<TimerBase>();
        private HashSet<TimerBase> _removeOnTheNext = new HashSet<TimerBase>();
        /// <summary>
        /// Function to get the delta time for updating timers
        /// </summary>
        /// <returns></returns>
        protected virtual float GetDeltaTime() => Time.unscaledDeltaTime;

        private void Update()
        {
            foreach(var timer in _removeOnTheNext)
            {
                _timers.Remove(timer);
            }
            _removeOnTheNext.Clear();

            foreach(var timer in _timers)
            {
                timer.OnUpdate(GetDeltaTime());
            }
        }

        public void StartUpdating(TimerBase timer)
        {
            if (!_timers.Contains(timer))
            {
                _timers.Add(timer);
            }
            if (_removeOnTheNext.Contains(timer))
            {
                _removeOnTheNext.Remove(timer);
            }
        }

        public void StopUpdating(TimerBase timer)
        {
            if (_timers.Contains(timer))
            {
                _removeOnTheNext.Add(timer);
            }
        }
    }
}
