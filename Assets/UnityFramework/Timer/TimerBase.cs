using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Timer
{
    public abstract class TimerBase
    {
        public abstract void OnUpdate(float deltaTime);

        /// <summary>
        /// Start the timer
        /// </summary>
        public void Start()
        {
            TimerUpdater.Instance.StartUpdating(this);
        }

        /// <summary>
        /// Stop the timer
        /// </summary>
        public void Stop()
        {
            Pause();
            Reset();
        }

        /// <summary>
        /// Pause the timer
        /// </summary>
        public void Pause()
        {
            TimerUpdater.Instance.StopUpdating(this);
        }

        public virtual void Reset()
        {

        }
    }
}
