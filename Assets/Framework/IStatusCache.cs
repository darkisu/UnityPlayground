using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    public interface IStatusCache
    {
        public bool Cached { get; }
        public void Cache();
        public void Restore();
    }
}
