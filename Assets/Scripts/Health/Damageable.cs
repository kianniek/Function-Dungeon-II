using Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Health
{
    public class Damageable : MonoBehaviour
    {
        [Header("Debug")]
        [SerializeField, ReadOnlyField] private float currentHealth;
        
        [Header("Settings")]
        [SerializeField] private float startHealth;
        [SerializeField] private bool enableNegativeHealth;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onDeathEvent = new();
        [SerializeField] private UnityEvent onDamageEvent = new();
        
        private void Awake()
        {
            currentHealth = startHealth;
        }
        
        /// <summary>
        /// The health of the object.
        /// </summary>
        public float Health
        {
            get => currentHealth;
            set
            {
                if (!enabled)
                    return;
                
                onDamageEvent.Invoke();
                
                if (!enableNegativeHealth && value < 0)
                    currentHealth = 0;
                else
                    currentHealth = value;
                
                if (value <= 0 && !enableNegativeHealth)
                    onDeathEvent.Invoke();
            }
        }
        
        /// <summary>
        /// Adds a listener to the onDamageEvent.
        /// </summary>
        /// <param name="action"> The function to add </param>
        public void SubscribeToDamageEvent(UnityAction action)
        {
            onDamageEvent.AddListener(action);
        }
        
        /// <summary>
        /// Adds a listener to the onDeathEvent.
        /// </summary>
        /// <param name="action"> The function to add </param>
        public void SubscribeToDeathEvent(UnityAction action)
        {
            onDeathEvent.AddListener(action);
        }
        
        /// <summary>
        /// Removes a listener from the onDamageEvent.
        /// </summary>
        /// <param name="action"> The function to remove </param>
        public void UnsubscribeFromDamageEvent(UnityAction action)
        {
            onDamageEvent.RemoveListener(action);
        }
        
        /// <summary>
        /// Removes a listener from the onDeathEvent.
        /// </summary>
        /// <param name="action"> The function to remove </param>
        public void UnsubscribeFromDeathEvent(UnityAction action)
        {
            onDeathEvent.RemoveListener(action);
        }
    }
}