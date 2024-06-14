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

        private LineRenderer _trajectoryLineRenderer;
        private float _a;
        private float _b;
        private LinearProjectile _activeProjectile;

        public float A
        {
            set
            {
                _a = value;
                
                UpdateLine();
            }
        }

        public float B
        {
            set
            {
                _b = value;
                
                UpdateLine();
            }
        }

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
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="linearProjectile"></param>
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
            _trajectoryLineRenderer = GetComponent<LineRenderer>();
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
            var direction = new Vector2 { x = 1, y = MathExtensions.LinearFunction(_a, _b, 1) }.normalized;

            var initialVelocity = direction * _activeProjectile.Speed;
            var gravityDelayPoint = direction * _activeProjectile.DelayValue;

            var trajectoryPoints = CalculateLinePoints(
                initialVelocity,
                gravityDelayPoint,
                -_activeProjectile.AppliedGravity
            );

            _trajectoryLineRenderer.positionCount = resolution;
            _trajectoryLineRenderer.SetPositions(trajectoryPoints);
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
                var timeStep = i * (length / resolution) / initialVelocity.x;

                nextPoint.x = initialVelocity.x * timeStep;
                nextPoint.y = initialVelocity.y * timeStep;

                // Apply gravity after the delay point
                if (applyDelayedGravity && nextPoint.sqrMagnitude >= gravityDelayPoint.sqrMagnitude)
                {
                    timeStep -= gravityDelayPoint.x / initialVelocity.x;
                    nextPoint.y -= 0.5f * gravity * timeStep * timeStep;
                }

                trajectoryPoints[i] = nextPoint;
            }

            return trajectoryPoints;
        }
    }
}