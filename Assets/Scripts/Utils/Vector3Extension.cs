using System;
using UnityEngine;

namespace Utils
{
    public static class Vector3Extension
    {
        /// <summary>
        /// Calculates the distance between 2 vectors using MathF.Sqrt instead of doubles
        /// </summary>
        /// <param name="from">The starting point</param>
        /// <param name="to">The ending point</param>
        /// <returns>The distance between 2 vectors</returns>
        public static float Distance(this Vector3 from, Vector3 to)
        {
            var x = from.x - to.x;
            var y = from.y - to.y;
            var z = from.z - to.z;

            return MathF.Sqrt(x * x + y * y + z * z);
        }
    }
}