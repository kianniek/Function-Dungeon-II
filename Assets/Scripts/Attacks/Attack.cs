using Events;
using UnityEngine;

namespace Attacks
{
    /// <summary>
    /// This class is responsible for defining the type of attack.
    /// </summary>
    public abstract class Attack : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private float startStrength = 1f;
        
        [Header("Events")]
        [SerializeField] private FloatEvent onStrengthChanged = new();
        
        private float _currentStrength;
        
        /// <summary>
        /// The current strength of the attack.
        /// </summary>
        public float CurrentStrength
        {
            get => _currentStrength;
            set
            {
                onStrengthChanged.Invoke(value);
                
                _currentStrength = value;
            }
        }
        
        private void Start()
        {
            CurrentStrength = startStrength;
        }
    }
}