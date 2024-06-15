using System.Collections;
using Events;
using Health;
using UnityEngine;
using UnityEngine.Events;

namespace Attacks
{
    public class AttackCollectiveHealthInRange : Attack
    {
        private readonly Collider[] _colliders = new Collider[10];
        
        [Header("Attack Settings")]
        [SerializeField, Min(0.1f)] private int attackSpeed;
        [SerializeField, Min(0.1f)] private float attackRange;
        
        [Header("Scan Settings")]
        [SerializeField, Min(0.1f)] private float targetScanInterval;
        
        [Header("Events")]
        [SerializeField] private GameObjectEvent onTargetFound = new();
        [SerializeField] private UnityEvent onTargetDead = new();
        
        private Damageable _target;
        private Transform _transform;
        private Coroutine _checkForTargetCoroutine;
        private Coroutine _attackCoroutine;

        private void Awake()
        {
            _transform = transform;
        }
        
        protected override void Start()
        {
            base.Start();
            
            _checkForTargetCoroutine = StartCoroutine(CheckForTargetCoroutine());
        }

        private void CheckForTarget()
        {
            var numberOfColliders = Physics.OverlapSphereNonAlloc(_transform.position, attackRange, _colliders);

            for (var i = 0; i < numberOfColliders; i++)
            {
                var possibleTarget = _colliders[i];
                
                if (!possibleTarget.TryGetComponent<CollectiveHealth>(out var collectiveHealth))
                    continue;

                var damageable = collectiveHealth.Damageable;

                if (!damageable.enabled)
                    continue;
                
                _target = damageable;
                _target.SubscribeToDeathEvent(ResetOnTargetDeath);
                
                StopCoroutine(_checkForTargetCoroutine);
                
                _attackCoroutine = StartCoroutine(AttackCoroutine());
                
                onTargetFound.Invoke(_target.gameObject);
            }
        }

        private void ResetOnTargetDeath()
        {
            StopCoroutine(_attackCoroutine);
            
            _target.UnsubscribeFromDeathEvent(ResetOnTargetDeath);
            _target = null;
            
            onTargetDead.Invoke();
            
            _checkForTargetCoroutine = StartCoroutine(CheckForTargetCoroutine());
        }
        
        private IEnumerator CheckForTargetCoroutine()
        {
            while (!_target)
            {
                CheckForTarget();
                
                yield return new WaitForSeconds(targetScanInterval);
            }
        }
        
        private IEnumerator AttackCoroutine()
        {
            while (_target)
            {
                _target.Health -= CurrentStrength;
                
                onImpact.Invoke();
                
                yield return new WaitForSeconds(attackSpeed);
            }
        }
    }
}