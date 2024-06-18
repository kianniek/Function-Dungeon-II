using Health;
using UnityEngine;

namespace Attacks
{
    /// <summary>
    /// This class is responsible for defining the behavior of a direct attack that triggers on impact.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class DirectAttackOnImpact3D : Attack
    {
        [Header("Damage Calculation Settings")]
        [SerializeField] private bool useVelocityForCalculation = true;
        
        private Rigidbody _rigidBody;
        
        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == gameObject)
                return;
            
            if (!other.TryGetComponent<Damageable>(out var damageableObject)) 
                return;
            
            var attackMultiplier = useVelocityForCalculation ? _rigidBody.velocity.magnitude : 1;
            
            var damage = CurrentStrength * attackMultiplier;
            
            damageableObject.Health -= damage;
            
            onImpact.Invoke();
        }
    }
}