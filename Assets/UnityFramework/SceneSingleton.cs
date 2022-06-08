using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    public abstract class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
    {
        private static T _internalInstance;

        /// <summary>
        /// The singleton instance of this monobehaviour.
        /// It will be created automatically if no instance is currently available.
        /// </summary>
        public static T Instance 
        { 
            get
            {
                if (_internalInstance == null)
                {
                    var gameObject = new GameObject(nameof(T));
                    gameObject.AddComponent<T>();
                }
                return _internalInstance;
            }
            set
            {
                _internalInstance = value;
            }
        }

        private void Awake()
        {
            if (_internalInstance == null)
            {
                _internalInstance = (T)this;
            }
            else if (_internalInstance != this)
            {
                Destroy(this);
            }
        }
        protected virtual void OnAwake()
        { }
    }
}
