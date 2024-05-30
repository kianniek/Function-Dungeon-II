using System.Collections;
using System.Collections.Generic;
using Attributes;
using Projectile;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class FunctionLineController : MonoBehaviour
{
    [Header("Function settings")]
    public bool isQuadratic;

    [Header("Function coefficients")]
    [Tooltip("This variable defines the slope of the function.")]
    [SerializeField] private float a = 1f;

    [Tooltip("This variable defines the y-intercept of the function.")]
    [SerializeField] private float b;

    [Tooltip("This variable defines the quadratic power of the function.")]
    [Min(0.001f)]
    [SerializeField] private float c = 1f;

    [Header("Visual settings")]
    [SerializeField] private float functionLineLength = 10f;
    [SerializeField] private int segments = 10;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Trajectory Settings")]
    [Header("References")]
    [SerializeField] private LineRenderer trajectoryLineRenderer;

    [Header("Variables")]
    [Min(0.0001f)]
    [SerializeField] private float timeStep = 0.1f; // Time step for calculating trajectory points

    //variable for the time the line should be hidden
    [SerializeField] private float hideTime = 5f;
    [SerializeField] private float groundLevel;
    [SerializeField, Expandable] private ProjectilePhysicsVariables projectilePhysicsVariables;

    public float GroundLevel
    {
        get => groundLevel;
        private set
        {
            groundLevel = value;
            CalculateTrajectory();
        }
    }

    public float TimeStep
    {
        get => timeStep;
        private set
        {
            timeStep = value;
            CalculateTrajectory();
        }
    }

    public float FunctionLineLength
    {
        get => functionLineLength;
        private set
        {
            functionLineLength = Mathf.Max(0, value); // Ensure lineLength never goes negative.
            UpdateFunction(); // Update the line renderer whenever the value changes.
        }
    }

    /// <summary>
    /// Sets the value of 'a' and updates the function.
    /// </summary>
    public float A
    {
        get => a;
        set
        {
            a = value;
            UpdateFunction();
        }
    }

    /// <summary>
    /// Sets the value of 'b' and updates the function.
    /// </summary>
    public float B
    {
        get => b;
        set
        {
            b = value;
            UpdateFunction();
        }
    }

    /// <summary>
    /// Sets the value of 'b' and updates the function.
    /// </summary>
    public float C
    {
        get => c;
        set
        {
            c = value;
            UpdateFunction();
        }
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Evaluates the function at a given 'x' value.
    /// </summary>
    /// <param name="x">The 'x' value at which to evaluate the function.</param>
    /// <returns>The 'y' value of the function at 'x'.</returns>
    public float EvaluateFunction(float x)
    {
        return a * Mathf.Pow(x, c) + b;
    }

    /// <summary>
    /// Computes the velocity at a point along the function.
    /// </summary>
    /// <returns>A vector representing the velocity.</returns>
    public Vector2 GetVelocityAtPoint()
    {
        return new Vector2(1, a).normalized * projectilePhysicsVariables.Velocity;
    }

    /// <summary>
    /// Draws the function line based on the current coefficients and settings.
    /// </summary>
    private void UpdateFunction()
    {
        if (!isQuadratic)
        {
            c = 1; // Forcing 'c' to be 1 if not quadratic
        }

        // Set the number of positions in the line renderer to match the number of segments and avoid exceeding the array bounds
        lineRenderer.positionCount = segments + 1;

        var direction = new Vector2(1, EvaluateFunction(1));
        var lineEndPoint = ProjectilePhysics.CalculateGraphTrajectoryEndPoint(direction, FunctionLineLength);

        // Iterate over each segment and evaluate the function
        for (var i = 0; i <= segments; i++)
        {
            var point = Vector2.Lerp(Vector2.zero, lineEndPoint, i / (float)segments);

            // Set the position of the line renderer
            lineRenderer.SetPosition(i, new(point.x, point.y, 0));
        }


        if (trajectoryLineRenderer != null && trajectoryLineRenderer.enabled)
        {
            CalculateTrajectory();
        }

    }

    /// <summary>
    /// Calculates the trajectory of the projectile based on the function and physics variables.
    /// </summary>
    private void CalculateTrajectory()
    {
        if (!trajectoryLineRenderer)
            return;

        // Calculate the direction and initial velocity of the projectile
        var direction = new Vector2(1, EvaluateFunction(1));
        var startPosition = CalculateStartPosition(direction);
        var initialVelocity = CalculateInitialVelocity(direction);

        // Calculate the time it takes for the projectile to reach the ground
        var timeToGround = CalculateTimeToGround(startPosition.y, initialVelocity.y);

        if (timeToGround < 0)
        {
            Debug.LogWarning("Calculated negative time to ground, which is invalid.");
            return;
        }

        UpdateTrajectoryLineRenderer(startPosition, initialVelocity, timeToGround);
    }

    /// <summary>
    /// Calculates the start position of the projectile based on the direction of the function.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private Vector2 CalculateStartPosition(Vector2 direction)
    {
        var clampedFollowDistance = Mathf.Clamp(projectilePhysicsVariables.FollowDistance, 0f, FunctionLineLength);
        var startPosition = ProjectilePhysics.CalculateGraphTrajectoryEndPoint(direction, clampedFollowDistance);
        return startPosition;
    }

    /// <summary>
    /// Calculates the initial velocity of the projectile based on the direction of the function.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private Vector2 CalculateInitialVelocity(Vector2 direction)
    {
        return ProjectilePhysics.CalculateGravityTrajectory(direction, projectilePhysicsVariables.Velocity, projectilePhysicsVariables.Gravity);
    }

    /// <summary>
    /// Calculates the time it takes for the projectile to hit the ground.
    /// </summary>
    /// <param name="initialHeight"></param>
    /// <param name="initialVerticalVelocity"></param>
    /// <returns></returns>
    private float CalculateTimeToGround(float initialHeight, float initialVerticalVelocity)
    {
        // Calculate the discriminant of the quadratic equation equation: b^2 - 4ac
        var discriminant = (initialVerticalVelocity * initialVerticalVelocity) -
                           4 * projectilePhysicsVariables.Gravity * (initialHeight - groundLevel);

        if (discriminant < 0)
        {
            Debug.LogWarning(
                "No real solutions, projectile does not hit the ground in a normal parabolic trajectory.");

            return -1; // Indicates no real solution
        }

        var timeToGround = (-initialVerticalVelocity - Mathf.Sqrt(discriminant)) /
                           -Mathf.Abs(projectilePhysicsVariables.Gravity);

        return timeToGround;
    }

    /// <summary>
    /// Updates the trajectory line renderer with the calculated trajectory points.
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="initialVelocity"></param>
    /// <param name="timeToGround"></param>
    private void UpdateTrajectoryLineRenderer(Vector3 startPosition, Vector2 initialVelocity, float timeToGround)
    {
        //Debug.Log($"initialVelocity {initialVelocity}");

        var calculatedSegments = Mathf.CeilToInt(timeToGround / timeStep);
        var trajectoryPoints = new List<Vector3>();

        // Calculate the trajectory points
        for (var i = 0; i <= calculatedSegments; i++)
        {
            // Calculate the displacement of the projectile
            var time = i * timeStep;

            if (time > timeToGround)
                time = timeToGround;

            // Calculate the displacement of the projectile formula: s = ut + 0.5 * a * t^2
            var displacement = new Vector3(
                initialVelocity.x * time,
                (initialVelocity.y * time) + 0.5f * (-Mathf.Abs(projectilePhysicsVariables.Gravity) * (time * time)),
                0);

            // Calculate the next point along the trajectory
            var nextPoint = startPosition + displacement;

            if (nextPoint.y < groundLevel)
            {
                nextPoint.y = groundLevel;
                trajectoryPoints.Add(nextPoint);
                break;
            }

            trajectoryPoints.Add(nextPoint);
        }

        trajectoryLineRenderer.positionCount = trajectoryPoints.Count;
        trajectoryLineRenderer.SetPositions(trajectoryPoints.ToArray());
    }

    /// <summary>
    /// Sets the visibility of the trajectory line renderer.
    /// </summary>
    /// <param name="enable"></param>
    public void TrajectoryLineIsVisible(bool enable)
    {
        trajectoryLineRenderer.enabled = enable;
        StartCoroutine(EnableTrajectoryLineAfterTimeOut(hideTime));
    }

    private IEnumerator EnableTrajectoryLineAfterTimeOut(float time)
    {
        yield return new WaitForSeconds(time);
        TrajectoryLineIsVisible(true);
    }

    private void OnValidate()
    {
        if (projectilePhysicsVariables == null)
            return;

        UpdateFunction();
        CalculateTrajectory();
    }
}