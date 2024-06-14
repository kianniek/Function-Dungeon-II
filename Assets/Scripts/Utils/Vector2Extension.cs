using System;
using UnityEngine;

namespace Utils
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

            return new Vector2 { x = xIntercept, y = yIntercept };
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

        /// <summary>
        /// Returns a point on a linear function given the x value, slope, and y-intercept.
        /// </summary>
        /// <param name="x">The x value in the linear function.</param>
        /// <param name="slope">The slope of the linear function.</param>
        /// <param name="yIntercept">The y-intercept of the linear function.</param>
        /// <returns>A Vector2 representing the point on the linear function corresponding to the given x value.</returns>
        public static Vector2 GetPoint(float x, float slope, float yIntercept)
        {
            return new Vector2 { x = x, y = MathExtensions.LinearFunctionY(x, slope, yIntercept) };
        }
    }
}