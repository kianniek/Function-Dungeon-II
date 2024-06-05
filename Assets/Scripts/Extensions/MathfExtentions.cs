using UnityEngine;

namespace Extensions
{
    public static class MathfExtentions
    {
        public static float RoundValue(float value, int amountBehindComma)
        {
            return Mathf.Round(value * Mathf.Pow(10, amountBehindComma)) / Mathf.Pow(10, amountBehindComma);
        }
    }
}
