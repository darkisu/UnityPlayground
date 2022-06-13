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
            var clampedElevation = Mathf.Clamp(elevation, nintyRad, -nintyRad);

            
            Vector3 result = new Vector3();
            result.y = radius * Mathf.Sin(clampedElevation);
            var xyProj = radius * Mathf.Cos(clampedElevation);
            result.x = xyProj * Mathf.Cos(polar);
            result.z = xyProj * Mathf.Sin(polar);
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
}