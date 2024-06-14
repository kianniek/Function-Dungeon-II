using UnityEngine;

namespace Extensions
{
    public static class MathExtensions
    {
        /// <summary>
        /// Rounds a value to a certain amount of decimal places.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="amountBehindComma">The amount of decimal places to round to.</param>
        /// <returns>The rounded value.</returns>
        public static float RoundValue(float value, int amountBehindComma)
        {
            return Mathf.Round(value * Mathf.Pow(10, amountBehindComma)) / Mathf.Pow(10, amountBehindComma);
        }
        
        /// <summary>
        /// Linear falloff function for a circle.
        /// </summary>
        /// <param name="magnitude"> The distance from the center of the circle. </param>
        /// <param name="radius"> The radius of the circle. </param>
        /// <returns> The falloff value. </returns>
        public static float CircleFallOff(float magnitude, float radius)
        {
            return (radius - magnitude) / radius;
        }
        
        /// <summary>
        /// The function of a line.
        /// </summary>
        /// <param name="a">The coefficient of the x term.</param>
        /// <param name="b">The constant term.</param>
        /// <param name="x">The x value of the line.</param>
        /// <returns>The y value of the line.</returns>
        public static float LinearFunction(float a, float b, float x)
        {
            return a * x + b;
        }
        
        /// <summary>
        /// The function of a parabola.
        /// </summary>
        /// <param name="a">The coefficient of the x^2 term.</param>
        /// <param name="b">The coefficient of the x term.</param>
        /// <param name="c">The constant term.</param>
        /// <param name="x">The x value of the parabola.</param>
        /// <returns>The y value of the parabola.</returns>
        public static float QuadraticFunction(float a, float b, float c, float x)
        {
            return a * x * x + b * x + c;
        }
    }
}
