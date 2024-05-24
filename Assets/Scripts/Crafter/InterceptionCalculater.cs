using LineController;
using UnityEngine;

public class InterceptionCalculater : MonoBehaviour
{
    [SerializeField] private GameObject line1;
    [SerializeField] private GameObject line2;

    public Vector2 intersection;

    private float _a1;
    private float _b1;

    private float _a2;
    private float _b2;

    private void Start()
    {
        _a1 = line1.GetComponent<FunctionLineController>().A;
        _b1 = line1.transform.position.y;

        _a2 = line2.GetComponent<FunctionLineController>().A;
        _b2 = line2.transform.position.y;

        intersection = FindIntersection(_a1, _b1, _a2, _b2);
    }

/// <summary>
/// Finds intersection between 2 lines
/// </summary>
/// <param name="a1">Slope of line 1</param>
/// <param name="b1">Intercept of line 1</param>
/// <param name="a2">Slope of line 2</param>
/// <param name="b2">Intercept of line 2</param>
/// <returns>The correct interception for 2 lines as vector2</returns>
    private Vector2 FindIntersection(float a1, float b1, float a2, float b2)
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
