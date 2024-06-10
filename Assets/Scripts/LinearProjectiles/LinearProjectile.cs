using System.Collections;
using Delay;
using Extensions;
using UnityEngine;

namespace LinearProjectiles
{
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
        
        private Vector3 _initialPosition;
        private float _initialGravityScale;
        
        public float GravityScale => _rigidBody2D.gravityScale;
        
        public float AppliedGravity => Physics2D.gravity.y * _rigidBody2D.gravityScale;
        
        public float Speed => speed;
        
        public float DelayValue => delayValue;
        
        public float DistanceTraveled => Vector2Extension.Distance(_initialPosition, _transform.position);
        
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
        
        private void Start()
        {
            Shoot(_transform.rotation);
        }
        
        private void FixedUpdate()
        {
            ManageInactivityCoroutine();
            
            if (delayedGravity && delayType == DelayType.DistanceBased && DistanceTraveled >= delayValue)
                _rigidBody2D.gravityScale = _initialGravityScale;
            
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
            _initialPosition = startPosition ? startPosition.position : _transform.position;
            
            _transform.position = _initialPosition;
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