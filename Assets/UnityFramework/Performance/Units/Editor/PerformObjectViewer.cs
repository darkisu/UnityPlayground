using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Darkisu.Performance.Editor
{
    public class PerformObjectViewer : EditorWindow
    {
        private EditorGUIStylePack _stylePack;

        [MenuItem(MenuItemSettings.WindowMenuPath + "Performance/Perform Object Viewer", priority = MenuItemSettings.WindowMenuPriority + 1)]
        public static void CreateAndShowWindow()
        {
            var window = GetWindow<PerformObjectViewer>();
            window.Show();
        }

        private void OnEnable()
        {
            _stylePack = new EditorGUIStylePack();
            _perfUnitCreator = new EditorPerformanceUnitCreator();
            _lastUpdateTime = (float)EditorApplication.timeSinceStartup;
            Selection.selectionChanged += Repaint;
        }
        private void OnDisable()
        {
            Selection.selectionChanged -= Repaint;
            _editorPerformanceUnit.OnStop();
            _editorPerformanceUnit = null;
        }

        private void OnGUI()
        {
            var selectedGameobject = Selection.activeGameObject;
            var inspectingPerformObject = selectedGameobject?.GetComponent<IPerformObject>();
            if (inspectingPerformObject != null && inspectingPerformObject.PerformanceUnit != null)
            {
                if (_editorPerformanceUnit == null || _editorPerformanceUnit.PerformanceUnit != inspectingPerformObject.PerformanceUnit)
                {
                    CreateEditorPerformanceUnit(inspectingPerformObject.PerformanceUnit);
                }
                DrawPerformanceControl(inspectingPerformObject.PerformanceUnit);
            }
            else if (inspectingPerformObject != null)
            {
                EditorGUILayout.LabelField($"No previewable performance unit on {selectedGameobject.name}.", _stylePack.LargeMessage);
            }
            else if (selectedGameobject != null)
            {
                EditorGUILayout.LabelField($"[{selectedGameobject.name}] is not a perform object.", _stylePack.LargeMessage);
            }
            else
            {
                EditorGUILayout.LabelField($"No perform object selected.", _stylePack.LargeMessage);
            }
        }

        private float _playbackSpeed = 1f;
        private float _time = 0f;
        private bool _isPlaying;
        private bool _isLooping;

        private void DrawPerformanceControl(IPerformanceUnit runtimePerformanceUnit)
        {
            _playbackSpeed = EditorGUILayout.FloatField("Previewing speed ", _playbackSpeed);
            var tempTime = Mathf.Max((float)EditorGUILayout.DoubleField("Previewing time ", _time), 0);
            if (tempTime != _time)
            {
                if (_time == 0)
                {
                    _editorPerformanceUnit.OnPlay();
                }
                _time = tempTime;
                _isPlaying = false;
            }
            
            _isLooping = EditorGUILayout.Toggle("Is previewing looping", _isLooping);

            EditorGUILayout.LabelField("Previewing Control");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Play"))
            {
                _isPlaying = true;
                _editorPerformanceUnit.OnPlay();
            }
            if (GUILayout.Button("Stop"))
            {
                _isPlaying = false;
                _time = 0;
                _editorPerformanceUnit.OnStop();
            }
            EditorGUILayout.EndHorizontal();
        }

        private float _lastUpdateTime;
        private void Update()
        {
            var deltaTime = (float)EditorApplication.timeSinceStartup - _lastUpdateTime;
            if (_editorPerformanceUnit != null)
            {
                if (_isPlaying)
                {
                    _time += deltaTime * _playbackSpeed;
                }

                if (!_editorPerformanceUnit.UpdateTo(_time))
                {
                    _time = 0f;
                    if (!_isLooping)
                    {
                        _isPlaying = false;
                    }
                }

                EditorWindow view = EditorWindow.GetWindow<SceneView>();
                //view.Repaint();
            }
            _lastUpdateTime = (float)EditorApplication.timeSinceStartup;
            Repaint();
        }

        private EditorPerformanceUnit _editorPerformanceUnit;
        private EditorPerformanceUnitCreator _perfUnitCreator;

        private void CreateEditorPerformanceUnit(IPerformanceUnit unit)
        {
            _editorPerformanceUnit = _perfUnitCreator.Create(unit);
            _playbackSpeed = unit.PlaybackSpeed;
        }
    }
}
