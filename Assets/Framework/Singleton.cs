using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    public class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _internalInstance;

        public static T Instance
        {
            get
            {
                if (_internalInstance == null)
                {
                    _internalInstance = new T();
                }
                return _internalInstance;
            }
            set
            {
                _internalInstance = value;
            }
        }
    }
}
