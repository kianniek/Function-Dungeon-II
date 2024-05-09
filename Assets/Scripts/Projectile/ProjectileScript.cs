using Targets;
using UnityEngine;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileScript : MonoBehaviour
    {
        [Header("Cannon settings")]
        [SerializeField] private int baseDamage = 1;
        [SerializeField] private float speed = 10f;
        [Header("Physic settings")]
        [Tooltip("The distance traveled until gravity is applied")]
        [SerializeField] private float maxDistance = 20f;
        [Tooltip("The gravity scale that will be applied when it has reached it max distance")]
        [SerializeField] private float gravityScale = 1f;
        [Tooltip("Time needed until the projectile is deactivated after time of inactivity")]
        [SerializeField] private float resetTime = 5f;

        private float _distanceTraveled;
        private float _currentResetTime;
        private Vector3 _initialPosition;
        private Vector3 _lastPosition;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _initialPosition = transform.position;
            _rb.velocity = transform.forward * speed;
            _rb.gravityScale = 0;
            _currentResetTime = resetTime;
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

            if (_distanceTraveled >= maxDistance)
            {
                EnablesGravity();
            }
        }

        /// <summary>
        /// Enables gravity by specifying a value other than zero
        /// </summary>
        private void EnablesGravity()
        {
            _rb.gravityScale = gravityScale;
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
            _currentResetTime = resetTime;
            transform.position = _initialPosition;
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
        }

        /// <summary>
        /// Launches the object towards a specified direction
        /// </summary>
        /// <param name="rotation">The rotation the projectile will be shot towards</param>
        public void Shoot(Quaternion rotation)
        {
            Vector2 direction = rotation * Vector2.right;
            _rb.AddForce(direction.normalized * speed);
        }
    }
}