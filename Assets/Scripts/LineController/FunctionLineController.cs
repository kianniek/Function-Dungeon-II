using System.Collections.Generic;
using Attributes;
using Projectile;
using UnityEngine;
using UnityEngine.Serialization;

//MVC pattern 
namespace LineController
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class FunctionLineController : MonoBehaviour
    {
        [Header("Function settings")] public bool isQuadratic;
        
        [Header("Function coefficients")] 
        [Tooltip("This variable defines the slope of the function.")] 
        [SerializeField] private float a = 1f;
        [Tooltip("This variable defines the y-intercept of the function.")] 
        [SerializeField] private float b;
        [Tooltip("This variable defines the quadratic power of the function.")] 
        [Min(0.001f)] 
        [SerializeField] private float c = 1f;
        
        [FormerlySerializedAs("functionmLineLength")]
        [FormerlySerializedAs("lineLength")]
        [Header("Visual settings")] 
        [SerializeField] private float functionLineLength = 10f;
        [SerializeField] private int segments = 10;
        [SerializeField] private LineRenderer lineRenderer;
        
        [Header("Trajectory Settings")] 
        
        [Header("Projectile Debug Settings")] 
        [SerializeField] private bool debugMode;
        // [SerializeField] private Vector2 projectileVelocity;
        
        [Header("References")] 
        [SerializeField] private LineRenderer trajectoryLineRenderer;
        
        [Header("Variables")] 
        [Min(0.0001f)] 
        [SerializeField] private float timeStep = 0.1f; // Time step for calculating trajectory points
        // [SerializeField] private float followDistance = 5f; // Distance along the function line to set the start point
        // [SerializeField] private float gravity = Physics.gravity.y; // Gravity value
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
        
        // public float FollowDistance
        // {
        //     get => followDistance;
        //     private set
        //     {
        //         followDistance = value;
        //         CalculateTrajectory();
        //     }
        // }
        //
        // public float Gravity
        // {
        //     get => gravity;
        //     private set
        //     {
        //         gravity = value;
        //         CalculateTrajectory();
        //     }
        // }
        
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
            
            if (debugMode)
            {
                Debug.LogWarning(
                    "Debug mode is active, which overrides variables for trajectory calculation. Turn off if debugging is not intended.");
            }
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
            
            // Calculate the step size for each segment
            var step = functionLineLength / segments;
            
            // Iterate over each segment and evaluate the function
            for (var i = 0; i <= segments; i++)
            {
                // Calculate the x and y values for the function
                var x = i * step;
                var y = EvaluateFunction(x);
                
                // Set the position of the line renderer
                lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }
            
            CalculateTrajectory();
        }
        
        // Calculates the trajectory of the projectile based on the initial conditions provided by the FunctionLineController
        private void CalculateTrajectory()
        {
            if (!trajectoryLineRenderer)
                return;
            
            // Get the initial velocity of the projectile
            var startFollowDistance = Mathf.Clamp(projectilePhysicsVariables.FollowDistance, 0f, FunctionLineLength);
            var startPosition = new Vector3(startFollowDistance, EvaluateFunction(startFollowDistance), 0);
            
            Vector3 initialVelocity = GetVelocityAtPoint();
            
            var initialVerticalVelocity = initialVelocity.y;
            var initialHeight = startPosition.y;
            
            // Calculate the discriminant to determine if the projectile hits the ground
            var discriminant = initialVerticalVelocity * initialVerticalVelocity -
                               2 * projectilePhysicsVariables.Gravity * (initialHeight - groundLevel);
            
            if (discriminant < 0)
            {
                Debug.LogWarning(
                    "No real solutions, projectile does not hit the ground in a normal parabolic trajectory.");
                
                return;
            }
            
            // Calculate the time it takes for the projectile to hit the ground
            var timeToGround = (-initialVerticalVelocity - Mathf.Sqrt(discriminant)) / projectilePhysicsVariables.Gravity;
            
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
                var displacement = new Vector3(initialVelocity.x * time,
                    initialVerticalVelocity * time + 0.5f * projectilePhysicsVariables.Gravity * time * time, 0);
                
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
        
        private void OnValidate()
        {
            UpdateFunction();
            CalculateTrajectory();
        }
    }
}