using UnityEngine;

namespace LinearFunction
{
    public static class FunctionToAngle
    {
        public static float GetAngleFromSlope(float slope)
        {
            var angleInRadians = Mathf.Atan(slope);

            var angleInDegrees = angleInRadians * Mathf.Rad2Deg;

            return angleInDegrees;
        }
    }
}
