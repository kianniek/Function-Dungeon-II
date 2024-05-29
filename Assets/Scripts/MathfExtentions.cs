using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathfExtentions 
{
    public static float RoundValue(this float value, int amountBehindComma)
    {
        return Mathf.Round(value * Mathf.Pow(10, amountBehindComma)) / Mathf.Pow(10, amountBehindComma);
    }
}
