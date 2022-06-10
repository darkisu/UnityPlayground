using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    public interface IStatusCache
    {
        public void Cache();
        public void Restore();
    }
}
