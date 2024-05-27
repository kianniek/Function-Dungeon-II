using Events.GameEvents.Typed;
using UnityEngine;

namespace Health
{
    [CreateAssetMenu(fileName = "HealthContainer", menuName = "Health Container")]
    public class HealthContainer : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField] private bool enableNegativeHealth;
        
        [Header("Game Events")]
        [SerializeField] private FloatGameEvent onHealthChanged;
        [SerializeField] private FloatGameEvent onDeath;
        
        private float _currentHealth;
        
        public float CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }
    }
}