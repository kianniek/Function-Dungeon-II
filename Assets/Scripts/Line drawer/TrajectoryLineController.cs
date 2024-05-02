using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TrajectoryLineController : MonoBehaviour
{
    [Header("Projectile Debug Settings")]
    [SerializeField] private bool _debug = false;
    [SerializeField] private Vector2 projectileVelocity;

    [Header("References")]
    [SerializeField] private LineRenderer trajectoryLineRenderer;
    [SerializeField] private FunctionLineController functionLineController;

    [Header("Variables")]
    [Min(0.0001f)]
    [SerializeField] private float followDistance = 5f; // Distance along the function line to set the start point
    [SerializeField] private float gravity = Physics.gravity.y; // Gravity value
    [SerializeField] private float groundLevel = 0;
    [SerializeField] private float timeStep = 0.1f; // Time step for calculating trajectory points

    void OnDrawGizmos()
    {
        DrawTrajectory();
        Gizmos.DrawRay(new Vector2(0, groundLevel), Vector2.right * Mathf.Infinity);
    }

    void Awake()
    {
        if (_debug)
        {
            Debug.LogWarning("Debug mode is active, which overrides variables for trajectory calculation. Turn off if debugging is not intended.");
        }
    }

    public void SetGroundLevel(float value)
    {
        groundLevel = value;
        DrawTrajectory();
    }

    public void DrawTrajectory()
    {
        if (!trajectoryLineRenderer || !functionLineController)
            return;

        followDistance = Mathf.Clamp(followDistance, 0f, functionLineController.GetLineLength());
        Vector3 startPosition = new Vector3(followDistance, functionLineController.EvaluateFunction(followDistance), 0);
        Vector3 initialVelocity = _debug ? projectileVelocity : functionLineController.GetVelocityAtPoint(startPosition, followDistance);

        float initialVerticalVelocity = initialVelocity.y;
        float initialHeight = startPosition.y;
        float discriminant = initialVerticalVelocity * initialVerticalVelocity - 2 * gravity * (initialHeight - groundLevel);

        if (discriminant < 0)
        {
            Debug.LogWarning("No real solutions, projectile does not hit the ground in a normal parabolic trajectory.");
            return;
        }

        float timeToGround = (-initialVerticalVelocity - Mathf.Sqrt(discriminant)) / gravity;
        if (timeToGround < 0)
        {
            Debug.LogWarning("Calculated negative time to ground, which is invalid.");
            return;
        }

        int calculatedSegments = Mathf.CeilToInt(timeToGround / timeStep);
        List<Vector3> trajectoryPoints = new List<Vector3>();

        for (int i = 0; i <= calculatedSegments; i++)
        {
            float time = i * timeStep;
            if (time > timeToGround)
                time = timeToGround;

            Vector3 displacement = new Vector3(initialVelocity.x * time, initialVerticalVelocity * time + 0.5f * gravity * time * time, 0);
            Vector3 nextPoint = startPosition + displacement;

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
}
