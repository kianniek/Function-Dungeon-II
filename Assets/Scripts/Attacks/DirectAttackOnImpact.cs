using Health;
using UnityEngine;

namespace Attacks
{
    /// <summary>
    /// This class is responsible for defining the behavior of a direct attack that triggers on impact.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class DirectAttackOnImpact : Attack
    {
        [Header("Damage Calculation Settings")]
        [SerializeField] private bool useVelocityForCalculation = true;
        
        private Rigidbody2D _rigidBody2D;
        
        private void Awake()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var attackMultiplier = useVelocityForCalculation ? _rigidBody2D.velocity.magnitude : 1;
            
            if (!collision.gameObject.TryGetComponent<Damageable>(out var damageableObject)) 
                return;
            
            var damage = CurrentStrength * attackMultiplier;
            
            damageableObject.Health -= damage;
            
            onImpact.Invoke();
        }
    }
}