using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.Tests
{
    public class ModifiableIntTest
    {
        [Test, Order(0)]
        public void OneLayerModifying(
            [Values(100)] int baseValue,
            [Values(10)] int realValue,
            [Values(-0.25f)] float ratioValue)
        {
            // base 100
            Func<int> baseValueFunc = () => baseValue;
            // plus 10
            ValueModifier<int> realValueModifier = (baseValue) => realValue;
            // minus 25%
            ValueModifier<int> ratioValueModifier = (baseValue) => (int)(baseValue * ratioValue);

            // test
            ModifiableInt modifiableInt = new ModifiableInt(baseValueFunc);
            modifiableInt.Add(realValueModifier);
            modifiableInt.Add(ratioValueModifier);

            // result 
            int result = baseValue + realValue + (int)(baseValue * ratioValue);
            Assert.AreEqual(result, modifiableInt.GetModifiedValue());
            modifiableInt.UpdateCachedModifiedValue();
            Assert.AreEqual(result, modifiableInt.CachedModifiedValue);
        }

        [Test, Order(1)]
        public void MultiLayerModifying(
            [Values(100)] int baseValue,
            [Values(10)] int realValue,
            [Values(-0.25f)] float ratioValue)
        {
            // base 100
            Func<int> baseValueFunc = () => baseValue;
            // plus 10
            ValueModifier<int> firstLayerModifier = (baseValue) => realValue;
            // minus 25%
            ValueModifier<int> secondLayerModifier = (baseValue) => (int)(baseValue * ratioValue);

            // test
            ModifiableInt firstLayer = new ModifiableInt(baseValueFunc);
            firstLayer.Add(firstLayerModifier);
            ModifiableInt secondLayer = new ModifiableInt(firstLayer);
            secondLayer.Add(secondLayerModifier);

            // result 
            int firstResult = baseValue + realValue;
            int secondResult = firstResult + (int)(firstResult * ratioValue);
            Assert.AreEqual(secondResult, secondLayer.GetModifiedValue());
        }

        [Test, Order(2)]
        public void OnTheGoModifying(
            [Values(100)] int baseValue,
            [Values(10)] int realValue,
            [Values(2)] int onTheGoDelta)
        {
            // base 100
            Func<int> baseValueFunc = () => baseValue;
            // plus 10
            ValueModifier<int> firstLayerModifier = (baseValue) => realValue;
            
            // test
            ModifiableInt modifiableInt = new ModifiableInt(baseValueFunc);
            modifiableInt.Add(firstLayerModifier);
            

            // result 
            int result = baseValue + realValue;
            Assert.AreEqual(result, modifiableInt.GetModifiedValue());
            realValue += onTheGoDelta;
            result = baseValue + realValue;
            Assert.AreEqual(result, modifiableInt.GetModifiedValue());
        }
    }
}
