using Health;
using UnityEngine;
using Utils;

namespace Attacks
{
    /// <summary>
    /// This class is responsible for defining the behavior of an area of effect attack that triggers on impact.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class AoeAttackOnImpact : Attack
    {
        [Header("Aoe Settings")]
        [SerializeField, Min(0.01f)] private float effectRadius;
        [SerializeField] private bool applyDamageFallOff = true;
        
        private Collider2D[] _hitColliders;
        
        private void OnCollisionEnter2D(Collision2D _)
        {
            _hitColliders = Physics2D.OverlapCircleAll(transform.position, effectRadius);
            
            foreach (var hitCollider in _hitColliders)
            {
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
            }
        }
    }
}