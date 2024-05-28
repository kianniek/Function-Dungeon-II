using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LinearFunction
{
    public static class LinearFunction
    {
        /// <summary>
        /// Returns the slope of a line given two points
        /// </summary>
        /// <param name="x"></param>
        /// <param name="slope"></param>
        /// <param name="yIntercept"></param>
        /// <returns></returns>
        public static float GetY(float x, float slope, float yIntercept)
        {
            return slope * x + yIntercept;
        }

        /// <summary>
        /// Returns the slope of a line given two points
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <returns></returns>
        public static float GetSlope(Vector2 point1, Vector2 point2)
        {
            return (point2.y - point1.y) / (point2.x - point1.x);
        }

        /// <summary>
        /// Returns the y-intercept of a line given a point and a slope
        /// </summary>
        /// <param name="point"></param>
        /// <param name="slope"></param>
        /// <returns></returns>
        public static float GetYIntercept(Vector2 point, float slope)
        {
            return point.y - slope * point.x;
        }

        /// <summary>
        /// Returns a point given an x value, slope, and y-intercept
        /// </summary>
        /// <param name="x"></param>
        /// <param name="slope"></param>
        /// <param name="yIntercept"></param>
        /// <returns></returns>
        public static Vector2 GetPoint(float x, float slope, float yIntercept)
        {
            return new Vector2(x, GetY(x, slope, yIntercept));
        }

        /// <summary>
        /// Returns the angle of a function given a slope
        /// </summary>
        /// <param name="slope"></param>
        /// <returns></returns>
        public static float GetAngleOfFunction(float slope)
        {
            // Calculate the angle in radians
            float angleInRadians = Mathf.Atan(slope);

            // Convert the angle to degrees
            float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

            return angleInDegrees;
        }
    }
}
