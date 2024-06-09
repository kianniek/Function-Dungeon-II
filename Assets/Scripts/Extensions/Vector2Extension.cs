using System;
using UnityEngine;

namespace Extensions
{
    public static class Vector2Extension
    {
        /// <summary>
        /// Finds intersection between 2 lines
        /// </summary>
        /// <param name="a1">Slope of line 1</param>
        /// <param name="b1">Intercept of line 1</param>
        /// <param name="a2">Slope of line 2</param>
        /// <param name="b2">Intercept of line 2</param>
        /// <returns>The correct interception for 2 lines as vector2</returns>
        public static Vector2 FindIntersection(float a1, float b1, float a2, float b2)
        {
            if (Mathf.Approximately(a1, a2))
            {
                Debug.Log("Lines are parallel and do not intersect.");
                
                return Vector2.zero;
            }
            
            var xIntercept = (b2 - b1) / (a1 - a2);
            var yIntercept = a1 * xIntercept + b1;
            
            return new Vector2(xIntercept, yIntercept);
        }
        
        /// <summary>
        /// Calculates the distance between 2 vectors using MathF.Sqrt instead of doubles
        /// </summary>
        /// <param name="from">The starting point</param>
        /// <param name="to">The ending point</param>
        /// <returns>The distance between 2 vectors</returns>
        public static float Distance(this Vector2 from, Vector2 to)
        {
            var x = from.x - to.x;
            var y = from.y - to.y;
            
            return MathF.Sqrt(x * x + y * y);
        }
    }
}
