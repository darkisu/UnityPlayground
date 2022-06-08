using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    /// <summary>
    /// A list that can store the value modification usually for buff/debuff systems. 
    /// </summary>
    /// <typeparam name="T">Type of the value, stricted to value types only.</typeparam>
    public abstract class ModifiableValue<T> : List<ValueModifier<T>>  where T : struct
    {
        /// <summary>
        /// method to get the modifier bases
        /// </summary>
        public Func<T> BaseValueFunc;

        /// <summary>
        /// The result of last call by UpdateCachedModifiedValue()
        /// </summary>
        public T CachedModifiedValue { get; private set; }
        /// <summary>
        /// Update the current modified value
        /// </summary>
        public void UpdateCachedModifiedValue()
        {
            CachedModifiedValue = GetModifiedValue();
        }
        /// <summary>
        /// Directly calculate the modified value
        /// </summary>
        /// <returns>Result that apply every calculation in the list.</returns>
        public T GetModifiedValue()
        {
            T baseValue = GetBaseValue();
            T result = baseValue;
            foreach (var modifier in this)
            {
                if (modifier != null)
                {
                    CalculateResult(modifier.Invoke(baseValue), ref result);
                }
                else
                {
                    CalculateResult(default(T), ref result);
                }
            }
            return result;
        }
        public T GetBaseValue()
        {
            return BaseValueFunc == null ? default : BaseValueFunc.Invoke();
        }
        protected abstract void CalculateResult(T modifierReturn, ref T result);

        public static implicit operator Func<T>(ModifiableValue<T> modifiableValue) => modifiableValue.GetModifiedValue;
    }
}
