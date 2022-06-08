using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Timer
{
    /// <summary>
    /// Recommended to use this timer instead of Monobehaviour.Update for better performance.
    /// </summary>
    public class PerUpdateTimer : TimerBase
    {
        public event Action<float> OnUpdateAction;
        public override void OnUpdate(float deltaTime)
        {
            OnUpdateAction?.Invoke(deltaTime);
        }
    }
}
