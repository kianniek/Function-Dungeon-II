using System.Collections;
using Delay;
using UnityEngine;
using Utils;

namespace LinearProjectiles
{
    /// <summary>
    /// This class is responsible for defining the behavior and type of linear projectile.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class LinearProjectile : MonoBehaviour
    {
        [Header("Projectile Settings")] 
        [SerializeField] private Transform startPosition;
        
        [Header("Physics Settings")] 
        [SerializeField, Min(1f)] private float speed = 10f;
        [SerializeField] private bool delayedGravity;
        [SerializeField] private DelayType delayType;
        [SerializeField, Min(1f)] private float delayValue;
        
        [Header("Deactivation Settings")] 
        [SerializeField, Min(1f)] private float resetOnInactivityTime = 5f;
        [SerializeField, Min(1f)] private float resetOnDistance = 100f;
        [SerializeField, Min(1f)] private float resetOnTime = 120f;
        
        private Transform _transform;
        private Rigidbody2D _rigidBody2D;
        private Vector3 _startPosition;
        private IEnumerator _resetOnInactivityCoroutine;
        
        private float _initialGravityScale = 1f;
        private bool _gravityApplied;
        
        /// <summary>
        /// The applied gravity of the projectile.
        /// </summary>
        public float AppliedGravity => Physics2D.gravity.y * _initialGravityScale;
        
        /// <summary>
        /// The speed of the projectile.
        /// </summary>
        public float Speed => speed;
        
        /// <summary>
        /// The delay value of the projectile.
        /// </summary>
        /// <remarks> Depending on the delay type, this value can represent either time or distance. </remarks>
        public float DelayValue => delayValue;

        /// <summary>
        /// Whether the projectile has delayed gravity.
        /// </summary>
        public bool DelayedGravity => delayedGravity;

        /// <summary>
        /// The type of delay used for gravity.
        /// </summary>
        public DelayType DelayType => delayType;
        
        private float DistanceTraveled => Vector2Extension.Distance(_startPosition, _transform.position);
        
        private bool ReachedGravityDelayPoint => !_gravityApplied && delayedGravity && 
                                                 delayType == DelayType.DistanceBased &&
                                                 DistanceTraveled >= delayValue;

        /// <summary>
        /// Shoots the projectile in the specified direction.
        /// </summary>
        /// <param name="rotation">The direction in which the projectile should be shot.</param>
        public void Shoot(Quaternion rotation)
        {
            SetInitialPositionAndRotation(rotation);
            SetInitialPhysicsProperties();
            
            StartCoroutine(ResetOnTime());
            
            if (!delayedGravity || delayType != DelayType.TimeBased)
                return;
            
            StartCoroutine(DelayGravity());
        }
        
        private void Awake()
        {
            _transform = transform;
            _rigidBody2D = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            ManageInactivityCoroutine();
            
            if (ReachedGravityDelayPoint)
            {
                _rigidBody2D.gravityScale = _initialGravityScale;
                _gravityApplied = true;
            }

            if (DistanceTraveled >= resetOnDistance)
                Reset();
        }

        private void Reset()
        {
            StopAllCoroutines();
            
            gameObject.SetActive(false);
        }
        
        private void SetInitialPhysicsProperties()
        {
            _initialGravityScale = _rigidBody2D.gravityScale;
            
            _rigidBody2D.gravityScale = delayedGravity ? 0 : _initialGravityScale;
            _rigidBody2D.velocity = _transform.right * speed;
        }
        
        private void SetInitialPositionAndRotation(Quaternion rotation)
        {
            _startPosition = startPosition ? startPosition.position : _transform.position;
            
            _transform.position = _startPosition;
            _transform.rotation = rotation;
        }
        
        private IEnumerator DelayGravity()
        {
            yield return new WaitForSeconds(delayValue);
            
            _rigidBody2D.gravityScale = _initialGravityScale;
        }
        
        private IEnumerator ResetOnTime()
        {
            yield return new WaitForSeconds(resetOnTime);
            
            Reset();
        }
        
        private IEnumerator ResetOnInactivity()
        {
            yield return new WaitForSeconds(resetOnInactivityTime);
            
            Reset();
        }
        
        private void ManageInactivityCoroutine()
        {
            if (_rigidBody2D.velocity.sqrMagnitude <= 0.1f)
            {
                if (_resetOnInactivityCoroutine != null)
                    return;
                
                _resetOnInactivityCoroutine = ResetOnInactivity();
                
                StartCoroutine(_resetOnInactivityCoroutine);
            }
            else if (_resetOnInactivityCoroutine != null)
            {
                StopCoroutine(_resetOnInactivityCoroutine);
                
                _resetOnInactivityCoroutine = null;
            }
        }
    }
}