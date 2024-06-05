using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
    public static class ColorBlockExtensions
    {
        public static ColorBlock GetColorBlock(Color color)
        {
            return new ColorBlock
            {
                normalColor = color,
                highlightedColor = color,
                pressedColor = color,
                selectedColor = color,
                disabledColor = color,
                colorMultiplier = 1,
                fadeDuration = 0.1f
            };
        }
    }
}