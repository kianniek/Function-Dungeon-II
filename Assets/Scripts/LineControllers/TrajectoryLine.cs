using Delay;
using Extensions;
using LinearProjectiles;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace LineControllers
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryLine : MonoBehaviour
    {
        [Header("Visual Settings")] 
        [SerializeField, Min(2)] private int resolution = 10;
        [SerializeField, Min(1)] private float length = 10f;

        private LineRenderer _lineRenderer;
        private float _a;
        private float _b;
        private LinearProjectile _activeProjectile;

        /// <summary>
        /// The coefficient of the x-term in the linear function.
        /// </summary>
        public float A
        {
            set
            {
                _a = value;
                
                UpdateLine();
            }
        }

        /// <summary>
        /// The constant term in the linear function.
        /// </summary>
        public float B
        {
            set
            {
                _b = value;
                
                UpdateLine();
            }
        }

        /// <summary>
        /// The active projectile that will be used to calculate the trajectory.
        /// </summary>
        public LinearProjectile ActiveProjectile
        {
            set
            {
                _activeProjectile = value;

                if (_activeProjectile) 
                    UpdateLine();
            }
        }
        
        /// <summary>
        /// Updates the line with the given x-coefficient, constant term and projectile physics.
        /// </summary>
        /// <param name="a"> The x-coefficient. </param>
        /// <param name="b"> The constant term. </param>
        /// <param name="linearProjectile"> The projectile physics. </param>
        public void UpdateLine(float a, float b, LinearProjectile linearProjectile)
        {
            _a = a; 
            _b = b;
            _activeProjectile = linearProjectile;

            if (_activeProjectile) 
                UpdateLine();
        }

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void OnValidate()
        {
            if (_activeProjectile) 
                UpdateLine();
            else
                Debug.LogWarning("No active projectile set for trajectory line.");
        }

        private void UpdateLine()
        {
            if (!_lineRenderer)
                return;
            
            var direction = new Vector2 { x = 1, y = MathExtensions.LinearFunction(_a, _b, 1) }.normalized;

            var initialVelocity = direction * _activeProjectile.Speed;
            var gravityDelayPoint = direction * _activeProjectile.DelayValue;

            var trajectoryPoints = CalculateLinePoints(
                initialVelocity,
                gravityDelayPoint,
                -_activeProjectile.AppliedGravity
            );

            _lineRenderer.positionCount = resolution;
            _lineRenderer.SetPositions(trajectoryPoints);
        }

        private Vector3[] CalculateLinePoints(Vector2 initialVelocity, Vector2 gravityDelayPoint, float gravity)
        {
            var trajectoryPoints = new Vector3[resolution];
            var nextPoint = Vector3.zero;
            var applyDelayedGravity = 
                _activeProjectile.DelayedGravity && 
                _activeProjectile.DelayType == DelayType.DistanceBased;

            for (var i = 0; i < resolution; i++)
            {
                // Calculate the time step for the next point
                var timeStep = i * (length / resolution) / initialVelocity.x;
                // Calculate the next point
                nextPoint.x = initialVelocity.x * timeStep;
                nextPoint.y = initialVelocity.y * timeStep;
                // Apply gravity after the delay point
                if (applyDelayedGravity && nextPoint.sqrMagnitude >= gravityDelayPoint.sqrMagnitude)
                {
                    // Update the time step to account for the delay
                    timeStep -= gravityDelayPoint.x / initialVelocity.x;
                    // Apply gravity
                    nextPoint.y -= 0.5f * gravity * timeStep * timeStep;
                }

                trajectoryPoints[i] = nextPoint;
            }

            return trajectoryPoints;
        }
    }
}