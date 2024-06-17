using UnityEngine;

namespace Health
{
    /// <summary>
    /// Represents a damageable object that can take damage from physics collisions.
    /// </summary>
    [RequireComponent(typeof(Damageable))]
    public class PhysicsDamageable : MonoBehaviour
    {
        [SerializeField] private float physicDamageThreshold = 2f;
        [SerializeField] private float physicDamageMultiplier = 1f;
        
        private Damageable _damageable;
        
        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent<PhysicsDamageable>(out _))
                return;
            
            var damage = collision.relativeVelocity.magnitude * physicDamageMultiplier;
            
            if (damage > physicDamageThreshold)
                _damageable.Health -= damage;
        }
    }
}