using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    public class EditorGUIStylePack
    {
        private GUIStyle _internalMiddleTitle;

        public GUIStyle MiddleTitle
        {
            get
            {
                if (_internalMiddleTitle == null)
                {
                    _internalMiddleTitle = new GUIStyle()
                    {
                        fontSize = 26,
                        alignment = TextAnchor.MiddleCenter,
                    };
                }
                return _internalMiddleTitle;
            }
        }
    }
}
