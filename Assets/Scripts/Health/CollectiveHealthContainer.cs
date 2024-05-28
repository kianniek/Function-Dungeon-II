using System.Collections.Generic;
using System.Linq;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Health
{
    /// <summary>
    /// Represents a container for health that can be shared between multiple damageable objects.
    /// </summary>
    [CreateAssetMenu(fileName = "Collective Health Container", menuName = "Collective Health Container")]
    public class CollectiveHealthContainer : ScriptableObject
    {
        private readonly List<Damageable> _damageables = new();
        
        [Header("Settings")] 
        [SerializeField] private bool enableNegativeHealth;
        
        [Header("Game Events")] 
        [SerializeField] private FloatGameEvent onHealthChanged;
        [SerializeField] private FloatGameEvent onDeath;
        
        private float _currentCollectiveHealth;
        
        /// <summary>
        /// The current collective health of all damageable objects.
        /// </summary>
        public float CurrentCollectiveHealth
        {
            get => _currentCollectiveHealth;
            private set
            {
                if (!enableNegativeHealth && value < 0)
                {
                    onDeath?.Invoke(value);
                    
                    _currentCollectiveHealth = 0;
                }
                else
                {
                    onHealthChanged?.Invoke(value);
                    
                    _currentCollectiveHealth = value;
                }
            }
        }
        
        /// <summary>
        /// Adds a damageable object to the health container.
        /// </summary>
        /// <param name="damageable">The damageable object to add.</param>
        public void AddDamageable(Damageable damageable)
        {
            _damageables.Add(damageable);
            
            damageable.SubscribeToDamageEvent(RecalculateTotalHealth);
            
            RecalculateTotalHealth();
        }
        
        /// <summary>
        /// Removes a damageable object from the health container.
        /// </summary>
        /// <param name="damageable">The damageable object to remove.</param>
        public void RemoveDamageable(Damageable damageable)
        {
            _damageables.Remove(damageable);
            
            damageable.UnsubscribeFromDamageEvent(RecalculateTotalHealth);
            
            RecalculateTotalHealth();
        }
        
        private void RecalculateTotalHealth()
        {
            CurrentCollectiveHealth = _damageables.Sum(damageable => damageable.Health);
        }
    }
}