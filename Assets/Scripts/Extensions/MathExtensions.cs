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
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float LinearFunction(float a, float b, float x)
        {
            return a * x + b;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float QuadraticFunction(float a, float b, float c, float x)
        {
            return a * x * x + b * x + c;
        }
    }
}
