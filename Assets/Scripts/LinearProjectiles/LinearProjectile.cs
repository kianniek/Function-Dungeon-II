using System.Collections;
using Delay;
using Extensions;
using UnityEngine;

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
        private IEnumerator _resetOnInactivityCoroutine;
        private bool _gravityApplied;

        /// <summary>
        /// 
        /// </summary>
        public float GravityScale { get; private set; } = 1f;

        /// <summary>
        /// 
        /// </summary>
        public float AppliedGravity => Physics2D.gravity.y * GravityScale;
        
        /// <summary>
        /// 
        /// </summary>
        public float Speed => speed;
        
        /// <summary>
        /// 
        /// </summary>
        public float DelayValue => delayValue;
        
        /// <summary>
        /// 
        /// </summary>
        public float DistanceTraveled => Vector2Extension.Distance(InitialPosition, _transform.position);
        
        /// <summary>
        /// 
        /// </summary>
        public Vector3 InitialPosition { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DelayedGravity => delayedGravity;

        /// <summary>
        /// 
        /// </summary>
        public DelayType DelayType => delayType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rotation"></param>
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
            
            if (
                !_gravityApplied &&
                delayedGravity && 
                delayType == DelayType.DistanceBased && 
                DistanceTraveled >= delayValue
            )
            {
                _rigidBody2D.gravityScale = GravityScale;
                
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
            GravityScale = _rigidBody2D.gravityScale;
            
            _rigidBody2D.gravityScale = delayedGravity ? 0 : GravityScale;
            _rigidBody2D.velocity = _transform.right * speed;
        }
        
        private void SetInitialPositionAndRotation(Quaternion rotation)
        {
            InitialPosition = startPosition ? startPosition.position : _transform.position;
            
            _transform.position = InitialPosition;
            _transform.rotation = rotation;
        }
        
        private IEnumerator DelayGravity()
        {
            yield return new WaitForSeconds(delayValue);
            
            _rigidBody2D.gravityScale = GravityScale;
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