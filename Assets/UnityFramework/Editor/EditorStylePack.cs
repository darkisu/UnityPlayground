using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    public class EditorGUIStylePack
    {
        private GUIStyle _internalLargeMessage;

        public GUIStyle LargeMessage
        {
            get
            {
                if (_internalLargeMessage == null)
                {
                    _internalLargeMessage = new GUIStyle()
                    {
                        fontSize = 26,
                        alignment = TextAnchor.MiddleCenter,
                        wordWrap = true,
                    };
                }
                return _internalLargeMessage;
            }
        }
    }
}
