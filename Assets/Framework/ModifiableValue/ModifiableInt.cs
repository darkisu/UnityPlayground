using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    /// <summary>
    /// Modifiable value for type int.
    /// ModifiedValue would be sum of all modifier;
    /// </summary>
    public class ModifiableInt : ModifiableValue<int>
    {
        public ModifiableInt()
        {
        }
        /// <summary>
        /// </summary>
        /// <param name="baseValueFunc">The function to get base value</param>
        public ModifiableInt(Func<int> baseValueFunc)
        {
            BaseValueFunc = baseValueFunc;
        }
        protected override void CalculateResult(int modifierReturn, ref int result)
        {
            result += modifierReturn;
        }
    }
}
