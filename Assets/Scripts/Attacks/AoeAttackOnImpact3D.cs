using System;
using Health;
using UnityEngine;
using Utils;

namespace Attacks
{
    /// <summary>
    /// This class is responsible for defining the behavior of an area of effect attack that triggers on impact.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class AoeAttackOnImpact3D : Attack
    {
        private readonly Collider[] _hitColliders = new Collider[10];
        
        [Header("Aoe Settings")]
        [SerializeField, Min(0.01f)] private float effectRadius;
        [SerializeField] private bool applyDamageFallOff = true;
        
        private void OnTriggerEnter(Collider other)
        {
            var colliders = Physics.OverlapSphereNonAlloc(transform.position, effectRadius, _hitColliders);

            for (var i = 0; i < colliders; i++)
            {
                var hitCollider = _hitColliders[i];
                
                if (hitCollider.gameObject == gameObject)
                    continue;
                
                if (!hitCollider.TryGetComponent<Damageable>(out var damageable))
                    continue;
                
                var colliderDirection = hitCollider.transform.position - transform.position;
                
                var damageFallOff = applyDamageFallOff
                    ? MathExtensions.CircleFallOff(colliderDirection.magnitude, effectRadius)
                    : 1;
                
                var damage = CurrentStrength * damageFallOff;
                
                damageable.Health -= damage;
                
                onImpact.Invoke();
            }
        }
    }
}