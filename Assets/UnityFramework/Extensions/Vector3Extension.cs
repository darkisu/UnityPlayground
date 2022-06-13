using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Darkisu
{
    /// <summary>
    /// Reference-
    /// https://blog.nobel-joergensen.com/2010/10/22/spherical-coordinates-in-unity/#:~:text=Spherical%20coordinate%20system%20is%20an,the%20origin%20to%20the%20point
    /// </summary>
    public static class Vector3ExtensionSphericalCoords
    { 
        /// <summary>
        /// Chage vector from spherical coordinates to cartesian
        /// </summary>
        /// <param name="sphericalVec">Vector3(radius, elevation, polar) in radians</param>
        /// <returns>Vactor3(x, y, z)</returns>
        public static Vector3 SphericalToCartesian(this Vector3 sphericalVec)
        {
            var radius = sphericalVec.x;
            var elevation = sphericalVec.y;
            var polar = sphericalVec.z;

            var nintyRad = 90 * Mathf.Deg2Rad;
            var clampedElevation = Mathf.Clamp(elevation, -nintyRad, nintyRad);

            
            Vector3 result = new Vector3();
            result.y = radius * Mathf.Sin(clampedElevation);
            var xyProj = radius * Mathf.Cos(clampedElevation);
            result.x = xyProj * Mathf.Sin(polar);
            result.z = xyProj * -Mathf.Cos(polar);
            return result;
        }
        /// <summary>
        /// Change vector from cartesian coordinates to spherical
        /// </summary>
        /// <param name="cartesianVec">Vector3(x, y, z)</param>
        /// <returns>Vector3(radius, elevation, polar)</returns>
        public static Vector3 CartesianToSpherical(this Vector3 cartesianVec)
        {
            var radius = cartesianVec.magnitude;

            var elevation = Mathf.Asin(cartesianVec.y / radius);
            var polar = Mathf.Atan(cartesianVec.z / cartesianVec.x);
            return new Vector3(radius, elevation, polar);
        }
    }

    [Serializable]
    public struct SphericalVector
    {
        [SerializeField]
        private float _radius;
        public float Radius
        {
            get => _radius;
            set
            {
                _radius = Mathf.Max(0, value);
            }
        }

        [SerializeField]
        private float _elevation;
        public float Elevation
        {
            get => _elevation;
            set
            {
                _elevation = Mathf.Clamp(value, -90, 90);
            }
        }
        [SerializeField]
        private float _polar;
        public float Polar
        {
            get => _polar;
            set
            {
                while (value < 0)
                {
                    value += 360f;
                }

                while (value > 360f)
                {
                    value -= 360f;
                }

                _polar = value;
            }
        }

        /// <summary>
        /// Get a vector3 version of this vector
        /// </summary>
        public Vector3 AsVector3
        {
            get => new Vector3(Radius, Elevation * Mathf.Deg2Rad, Polar * Mathf.Deg2Rad);
        }

        /// <summary>
        /// Call it in Monobehiviuor.OnValidate for input restriction
        /// </summary>
        public void OnValidate()
        {
            Radius = _radius;
            Elevation = _elevation;
            Polar = _polar;
        }
    }
}