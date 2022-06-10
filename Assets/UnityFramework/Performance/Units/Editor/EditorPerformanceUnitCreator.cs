using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Darkisu.Performance.Editor
{
    public class EditorPerformanceUnitCreator
    {
        private Dictionary<string, Type> _runtimeUnitNameToEditorType = new Dictionary<string, Type>();

        public EditorPerformanceUnitCreator()
        {
            var editorPerformanceType = typeof(EditorPerformanceUnit);
            var types = Assembly.GetAssembly(editorPerformanceType)
                .GetTypes()
                .Where((target) => target.IsSubclassOf(editorPerformanceType) || target == editorPerformanceType);
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<EditorPerformanceUnitAttribute>();
                if (attr != null && attr.IsValid)
                {
                    _runtimeUnitNameToEditorType[attr.RuntimePerformanceUnitType.Name] = type;
                }
            }
        }

        public EditorPerformanceUnit Create(IPerformanceUnit unit)
        {
            if (unit == null)
            {
                return null;
            }
            var unitType = unit.GetType();
            while(unitType.IsSubclassOf(typeof(IPerformanceUnit)))
            {
                if (_runtimeUnitNameToEditorType.TryGetValue(unitType.Name, out var type))
                {
                    return (EditorPerformanceUnit)type.GetConstructor(new[] { unitType }).Invoke(new[] { unit });
                }
            }
            var defaultEditorUnitType = _runtimeUnitNameToEditorType[nameof(IPerformanceUnit)];
            var constructor = defaultEditorUnitType.GetConstructor(new[] { typeof(IPerformanceUnit) });
            return (EditorPerformanceUnit)constructor.Invoke(new[] { unit });
        }
    }
}
