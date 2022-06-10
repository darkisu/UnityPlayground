using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Performance
{
    public interface IPerformanceUnit
    {
        /// <summary>
        /// The play time of the unit in seconds.
        /// </summary>
        public float Time { get; set; }
        /// <summary>
        /// Is this unit currently playing?
        /// The IsPlaying definition should be if it's updating or showing
        /// </summary>
        public bool IsPlaying { get; }
        /// <summary>
        /// The playback speed while self updating
        /// </summary>
        public float PlaybackSpeed { get; set; }
        /// <summary>
        /// Is this unit currently updating itself
        /// </summary>
        public bool IsSelfUpdating { get; set; }
        /// <summary>
        /// Start playing the unit
        /// </summary>
        public void Play();
        /// <summary>
        /// Stop the unit
        /// Use IPerformanceUnit.Time = 0 for pausing
        /// </summary>
        public void Stop();
    }
}
