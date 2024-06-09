using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
    public static class ColorBlockExtensions
    {
        /// <summary>
        /// Returns a ColorBlock with all colors set to the same color.
        /// </summary>
        /// <param name="color"> The color to set all fields to. </param>
        /// <param name="colorMultiplier"> The color multiplier to set all fields to. </param>
        /// <param name="fadeDuration"> The fade duration to set all fields to. </param>
        /// <returns> A ColorBlock with all colors set to the same color. </returns>
        public static ColorBlock GetColorBlock(Color color, float colorMultiplier = 1f, float fadeDuration = 0.1f)
        {
            return new ColorBlock
            {
                normalColor = color,
                highlightedColor = color,
                pressedColor = color,
                selectedColor = color,
                disabledColor = color,
                colorMultiplier = colorMultiplier,
                fadeDuration = fadeDuration
            };
        }
    }
}