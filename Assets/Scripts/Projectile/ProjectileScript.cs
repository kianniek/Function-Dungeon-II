using Attributes;
using Targets;
using UnityEngine;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileScript : MonoBehaviour
    {
        [Header("Cannon settings")]
        [SerializeField] private int baseDamage = 1;

        [Header("Projectile settings")]
        [SerializeField] private int projectileForceRadius = 2;
        [SerializeField] private int projectileForcePower = 20000;
        [SerializeField] private int projectileScore;
        public int ProjectileScore => projectileScore;

        // [SerializeField] private float speed = 10f;
        [Header("Physic settings")]
        [SerializeField, Expandable] private ProjectilePhysicsVariables physicsVariables;

        // [Tooltip("The distance traveled until gravity is applied")]
        // [SerializeField] private float maxDistance = 20f;
        // [Tooltip("The gravity scale that will be applied when it has reached it max distance")]
        // [SerializeField] private float gravityScale = 1f;
        [Tooltip("Time needed until the projectile is deactivated after time of inactivity")]
        [SerializeField] private float resetTime = 5f;

        private bool _firstCollision = true;
        private float _distanceTraveled;
        private float _currentResetTime;
        private Vector3 _initialPosition;
        private Vector3 _lastPosition;
        private Rigidbody2D _rb;
        private Vector2 _direction;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _initialPosition = transform.position;
            _rb.velocity = transform.forward * physicsVariables.Velocity;
            _rb.gravityScale = 0;
            _currentResetTime = resetTime;
            _lastPosition = transform.position;
        }

        private void FixedUpdate()
        {
            CalculateDistanceTraveled();
            CalculateDeactivationTimer();
        }

        /// <summary>
        /// Calculates the amount of distance the projectile has traveled, used to enable gravity after a certain distance
        /// </summary>
        private void CalculateDistanceTraveled()
        {
            var displacement = transform.position - _lastPosition;

            _distanceTraveled += displacement.magnitude;
            _lastPosition = transform.position;

            if (_distanceTraveled >= ProjectilePhysics
                    .CalculateGraphTrajectoryEndPoint(_direction, physicsVariables.FollowDistance).magnitude)
            {
                EnablesGravity();
            }
        }

        /// <summary>
        /// Enables gravity by specifying a value other than zero
        /// </summary>
        private void EnablesGravity()
        {
            Physics2D.gravity = new Vector2(0, -Mathf.Abs(physicsVariables.Gravity));
            _rb.gravityScale = 1;
        }

        /// <summary>
        /// Calculates the time before the objects gets deactavted due to inactivity (not Moving)
        /// </summary>
        private void CalculateDeactivationTimer()
        {
            if (_rb.velocity.magnitude <= 0.1f)
            {
                _currentResetTime -= Time.deltaTime;

                if (_currentResetTime <= 0f)
                {
                    ResetAndDeactivate();
                }
            }
            else
            {
                _currentResetTime = resetTime;
            }
        }

        /// <summary>
        /// Resets the variables to their default and deactivates the gameObject
        /// </summary>
        private void ResetAndDeactivate()
        {
            _distanceTraveled = 0f;
            _rb.velocity = Vector3.zero;
            _rb.gravityScale = 0f;
            _currentResetTime = resetTime;
            transform.position = _initialPosition;
            transform.rotation = Quaternion.identity;
            _lastPosition = Vector3.zero;
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            EnablesGravity();

            var hitScript = collision.gameObject.GetComponent<HitScript>();

            if (hitScript != null)
            {
                var speed = _rb.velocity.magnitude;
                hitScript.OnBlockHit(baseDamage * speed);
            }

            //Explosion for first collision
            if (_firstCollision)
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, projectileForceRadius);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.attachedRigidbody != null && hitCollider.attachedRigidbody != gameObject.GetComponent<Rigidbody2D>())
                    {
                        Vector2 direction = hitCollider.transform.position - transform.position;
                        float forceFalloff = 1 - (direction.magnitude / projectileForceRadius);
                        hitCollider.attachedRigidbody.AddForce(direction.normalized * (forceFalloff <= 0 ? 0 : projectileForcePower) * forceFalloff);
                    }
                }
                _firstCollision = false;
            }
        }

        /// <summary>
        /// Launches the object towards a specified direction
        /// </summary>
        /// <param name="rotation">The rotation the projectile will be shot towards</param>
        public void Shoot(Quaternion rotation)
        {
            _lastPosition = transform.position;
            _direction = (rotation * Vector2.right).normalized;
            _rb.AddForce(_direction * physicsVariables.Velocity, ForceMode2D.Impulse);
        }
    }
}