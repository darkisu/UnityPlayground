using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu.BillboardObject
{
    public class SphericalCameraControl : MonoBehaviour
    {
        public SphericalVector SphericalVector;

        private void OnValidate()
        {
            SphericalVector.OnValidate();
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            var position = SphericalVector.AsVector3.SphericalToCartesian();
            transform.localPosition = position;
            transform.forward = position.sqrMagnitude == 0 ? transform.forward : -position;
        }
    }
}
