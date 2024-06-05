using UnityEngine;

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
        if (a1 == a2)
        {
            Debug.Log("Lines are parallel and do not intersect.");
            return Vector2.zero;
        }

        var xIntercept = (b2 - b1) / (a1 - a2);
        var yIntercept = a1 * xIntercept + b1;

        return new Vector2(xIntercept, yIntercept);
    }
}
