using System.Collections.Generic;
using UnityEngine;

namespace LineController
{
    /// <summary>
    /// Controls the visualization of projectile trajectories based on initial conditions provided by a FunctionLineController.
    /// </summary>
    [ExecuteInEditMode]
    public class TrajectoryLineController : MonoBehaviour
    {
        [Header("Projectile Debug Settings")]
        [SerializeField] private bool debugMode;
        [SerializeField] private Vector2 projectileVelocity;

        [Header("References")]
        [SerializeField] private LineRenderer trajectoryLineRenderer;
        [SerializeField] private FunctionLineController functionLineController;

        [Header("Variables")]
        [Min(0.0001f)]
        [SerializeField] private float timeStep = 0.1f; // Time step for calculating trajectory points
        [SerializeField] private float followDistance = 5f; // Distance along the function line to set the start point
        [SerializeField] private float gravity = Physics.gravity.y; // Gravity value
        [SerializeField] private float groundLevel;

        public float GroundLevel
        {
            get => groundLevel;
            private set
            {
                groundLevel = value;
                CalculateTrajectory();
            }
        }

        public float FollowDistance
        {
            get => followDistance;
            private set
            {
                followDistance = value;
                CalculateTrajectory();
            }
        }

        public float Gravity
        {
            get => gravity;
            private set
            {
                gravity = value;
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

        /// <summary>
        /// Warns about debug mode being active upon initialization.
        /// </summary>
        private void Awake()
        {
            if (debugMode)
            {
                Debug.LogWarning("Debug mode is active, which overrides variables for trajectory calculation. Turn off if debugging is not intended.");
            }
        }

        /// <summary>
        /// Calculates the trajectory of the projectile based on the initial conditions provided by the FunctionLineController
        /// </summary>
        public void CalculateTrajectory()
        { 
            if (!trajectoryLineRenderer || !functionLineController)
                return;
            
            // Get the initial velocity of the projectile
            followDistance = Mathf.Clamp(followDistance, 0f, functionLineController.LineLength);
            var startPosition = new Vector3(followDistance, functionLineController.EvaluateFunction(followDistance), 0);
            Vector3 initialVelocity = debugMode ? projectileVelocity : functionLineController.GetVelocityAtPoint(startPosition, followDistance);
            var initialVerticalVelocity = initialVelocity.y;
            var initialHeight = startPosition.y;

            // Calculate the discriminant to determine if the projectile hits the ground
            var discriminant = initialVerticalVelocity * initialVerticalVelocity - 2 * gravity * (initialHeight - groundLevel);

            if (discriminant < 0)
            {
                Debug.LogWarning("No real solutions, projectile does not hit the ground in a normal parabolic trajectory.");
                return;
            }

            // Calculate the time it takes for the projectile to hit the ground
            var timeToGround = (-initialVerticalVelocity - Mathf.Sqrt(discriminant)) / gravity;
            if (timeToGround < 0)
            {
                Debug.LogWarning("Calculated negative time to ground, which is invalid.");
                return;
            }

            // Calculate the trajectory points based on the time it takes to hit the ground and the time step
            var calculatedSegments = Mathf.CeilToInt(timeToGround / timeStep);
            var trajectoryPoints = new List<Vector3>();

            for (var i = 0; i <= calculatedSegments; i++)
            {
                // Calculate the displacement of the projectile at the current time
                var time = i * timeStep;
                if (time > timeToGround)
                    time = timeToGround;

                // Calculate the next point in the trajectory
                var displacement = new Vector3(initialVelocity.x * time, initialVerticalVelocity * time + 0.5f * gravity * time * time, 0);
                var nextPoint = startPosition + displacement;

                // If the next point is below the ground level, set it to the ground level and add it to the trajectory
                if (nextPoint.y < groundLevel)
                {
                    nextPoint.y = groundLevel;
                    trajectoryPoints.Add(nextPoint);
                    break;
                }
                trajectoryPoints.Add(nextPoint);
            }

            // Set the trajectory points to the line renderer
            trajectoryLineRenderer.positionCount = trajectoryPoints.Count;
            trajectoryLineRenderer.SetPositions(trajectoryPoints.ToArray());
        }

        /// <summary>
        /// Automatically updates trajectory visualization when properties are changed in the editor.
        /// </summary>
        private void OnValidate()
        {
            CalculateTrajectory();
        }
    }
}