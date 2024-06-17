using Events.GameEvents;
using Events.GameEvents.Typed;
using LineControllers;
using UnityEngine;

//TODO class probably needs to be adjusted when ROBB-E is implemented, i dont know how the line will looks like atm.
public class LineCircleCollisionCheck : MonoBehaviour
{
    private Vector2 _sphereCenter; 
    private float _sphereRadius;

    [SerializeField] private GameEvent onHitpointHit;
    [SerializeField] private GameEvent onHitpointMiss;

    private float _a; //TODO assign this when line is implemented
    private float _b; //TODO assign this when line is implemented
    private GameObjectGameEvent onHandShot; //TODO make with ROBB-E
    private LinearGraphLine _linearGraphLine;//TODO assign when line is implemented

    private void Start()
    {
        _sphereRadius = transform.localScale.x;
        _sphereCenter = transform.position;
        onHandShot.AddListener(GetLine);
        onHandShot.AddListener(IntersectionDebugger);
    }

    //TODO possible change this when line is implemented
    private void GetLine(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<LinearGraphLine>(out _))
        {
            _linearGraphLine = gameObject.GetComponent<LinearGraphLine>();
        }
    }

    /// <summary>
    /// Debugs interception lines, calls LineIntersectsSphere. Should also be changed later
    /// </summary>
    private void IntersectionDebugger()
    {
        if (_linearGraphLine == null)
            return;

        _a = _linearGraphLine.A;
        _b = _linearGraphLine.B;

        Vector2 intersectionPoint1, intersectionPoint2;
        bool intersects = LineIntersectsSphere(out intersectionPoint1, out intersectionPoint2);

        if (intersects)
        {
            onHitpointHit.Invoke();
            Debug.Log("The line intersects with the sphere.");
            Debug.Log("Intersection point 1: " + intersectionPoint1);
            Debug.Log("Intersection point 2: " + intersectionPoint2);
        }
        else
        {
            onHitpointMiss.Invoke();
            Debug.Log("The line does not intersect with the sphere.");
        }
    }

    /// <summary>
    /// Uses discriminant to get if Line has collision with sphere
    /// </summary>
    /// <param name="intersectionPoint1"></param>
    /// <param name="intersectionPoint2"></param>
    /// <returns>True if the line has 1 or 2 intersection points with the circle</returns>
    public bool LineIntersectsSphere(out Vector2 intersectionPoint1, out Vector2 intersectionPoint2)
    {
        // Center of the sphere
        float xc = _sphereCenter.x;
        float yc = _sphereCenter.y;

        // Calculate discriminant values
        float A = 1 + _a * _a;
        float B = 2 * (_a * (_b - yc) - xc);
        float C = xc * xc + (_b - yc) * (_b - yc) - _sphereRadius * _sphereRadius;

        // Calculate discriminant
        float discriminant = B * B - 4 * A * C;

        if (discriminant < 0)
        {
            // No intersection
            intersectionPoint1 = Vector2.zero;
            intersectionPoint2 = Vector2.zero;
            return false;
        }
        else if (discriminant == 0)
        {
            // One intersection point (tangent)
            float t = -B / (2 * A);
            intersectionPoint1 = new Vector2(_a * t, _b + t);
            intersectionPoint2 = Vector2.zero;
            return true;
        }
        else
        {
            // Two intersection points
            float sqrtDiscriminant = Mathf.Sqrt(discriminant);
            float t1 = (-B + sqrtDiscriminant) / (2 * A);
            float t2 = (-B - sqrtDiscriminant) / (2 * A);

            intersectionPoint1 = new Vector2(_a * t1, _b + t1);
            intersectionPoint2 = new Vector2(_a * t2, _b + t2);
            return true;
        }
    }
}