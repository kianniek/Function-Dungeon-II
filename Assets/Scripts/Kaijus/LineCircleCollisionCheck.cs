using Events.GameEvents;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Kaijus
{
    public class LineCircleCollisionCheck : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private Vector2GameEvent onHitpointHit;
        [SerializeField] private GameEvent onHitpointMiss;
        [SerializeField] private Vector2GameEvent onHandShot;

        private float _a;
        private float _b;
        private Vector2 _sphereCenter;
        private float _sphereRadius;

        /// <summary>
        /// Get radius and center values form sphere and add event listeners.
        /// </summary>
        private void Start()
        {
            _sphereRadius = transform.localScale.x;
            _sphereCenter = transform.position;
            onHandShot.AddListener(LineIntersectsSphere);
        }

        private void OnDisable()
        {
            onHandShot.RemoveListener(LineIntersectsSphere);
        }

        /// <summary>
        /// Uses discriminant to get if Line has collision with sphere
        /// </summary>
        private void LineIntersectsSphere(Vector2 abValues)
        {
            _a = abValues.x;
            _b = abValues.y;
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
                onHitpointMiss.Invoke();
            }
            else
            {
                onHitpointHit.Invoke(transform.position);
                Destroy(gameObject);
            }
        }
    }
}