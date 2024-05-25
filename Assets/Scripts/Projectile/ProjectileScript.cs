using Attributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileScript : MonoBehaviour
    {
        [Header("Damage Settings")]
        [SerializeField] private int baseDamage = 1;
        
        [Header("Impact Settings")]
        [SerializeField] private int forceRadius = 5; 
        [SerializeField] private int forcePower = 10000;
        
        [Header("Physic settings")]
        [SerializeField, Expandable] private ProjectilePhysicsVariables physicsVariables;
        
        [Header("Reset Settings")]
        [Tooltip("Time needed until the projectile is deactivated after time of inactivity")]
        [SerializeField] private float resetTime = 5f;
        
        [Header("Events")]
        [SerializeField] private UnityEvent changeCameraView = new();
        
        private float _distanceTraveled;
        private float _currentResetTime;
        private Vector3 _initialPosition;
        private Vector3 _lastPosition;
        private Rigidbody2D _rb;
        private Vector2 _direction;
        
        public UnityEvent ChangeCameraView => changeCameraView;
        
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
        
        // Calculates the amount of distance the projectile has traveled, used to enable gravity after a certain distance
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
        
        // Enables gravity by specifying a value other than zero
        private void EnablesGravity()
        {
            Physics2D.gravity = new Vector2(0, -Mathf.Abs(physicsVariables.Gravity));
            _rb.gravityScale = 1;
        }
        
        // Calculates the time before the objects gets deactivated due to inactivity (not Moving)
        private void CalculateDeactivationTimer()
        {
            if (_rb.velocity.magnitude <= 0.1f)
            {
                _currentResetTime -= Time.deltaTime;

                if (_currentResetTime <= 0f) 
                    ResetAndDeactivate();
            }
            else
            {
                _currentResetTime = resetTime;
            }
        }
        
        // Resets the variables to their default and deactivates the gameObject
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
            if (collision.gameObject.TryGetComponent<Damageable>(out var damageableObject))
            {
                var speed = _rb.velocity.magnitude;
                
                damageableObject.Health -= baseDamage * speed;
            }

            //Explosion for first collision
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, forceRadius);
            
            foreach (var hitCollider in hitColliders)
            {
                var attachedRigidBody = hitCollider.attachedRigidbody;
                
                if (!attachedRigidBody || attachedRigidBody == _rb) 
                    continue;
                
                var direction = hitCollider.transform.position - transform.position;
                var forceFalloff = 1 - direction.magnitude / forceRadius;
                
                attachedRigidBody.AddForce(direction.normalized * (forceFalloff <= 0 ? 0 : forcePower) * forceFalloff);
            }
            
            changeCameraView.Invoke();
            
            ResetAndDeactivate();
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
