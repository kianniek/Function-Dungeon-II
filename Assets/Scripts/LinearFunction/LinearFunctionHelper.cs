using UnityEngine;

namespace LinearFunction
{
    public static class LinearFunctionHelper
    {
        /// <summary>
        /// Returns the y value of a linear function given the x value, slope, and y-intercept.
        /// </summary>
        /// <param name="x">The x value in the linear function.</param>
        /// <param name="slope">The slope of the linear function.</param>
        /// <param name="yIntercept">The y-intercept of the linear function.</param>
        /// <returns>The y value corresponding to the given x value in the linear function.</returns>
        public static float GetY(float x, float slope, float yIntercept)
        {
            return slope * x + yIntercept;
        }

        /// <summary>
        /// Returns the slope of a linear function given two points.
        /// </summary>
        /// <param name="point1">The first point in the form of a Vector2.</param>
        /// <param name="point2">The second point in the form of a Vector2.</param>
        /// <returns>The slope of the line passing through the two given points.</returns>
        public static float GetSlope(Vector2 point1, Vector2 point2)
        {
            return (point2.y - point1.y) / (point2.x - point1.x);
        }

        /// <summary>
        /// Returns the y-intercept of a linear function given a point and a slope.
        /// </summary>
        /// <param name="point">A point on the line in the form of a Vector2.</param>
        /// <param name="slope">The slope of the linear function.</param>
        /// <returns>The y-intercept of the linear function.</returns>
        public static float GetYIntercept(Vector2 point, float slope)
        {
            return point.y - slope * point.x;
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
            return new Vector2(x, GetY(x, slope, yIntercept));
        }

        /// <summary>
        /// Returns the angle of a linear function given the slope.
        /// </summary>
        /// <param name="slope">The slope of the linear function.</param>
        /// <returns>The angle in degrees of the linear function relative to the x-axis.</returns>
        public static float GetAngleOfFunction(float slope)
        {
            var angleInRadians = Mathf.Atan(slope);
            return angleInRadians * Mathf.Rad2Deg;
        }
    }
}
