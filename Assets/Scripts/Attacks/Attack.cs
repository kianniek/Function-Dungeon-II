using Events;
using UnityEngine;

namespace Attacks
{
    public abstract class Attack : MonoBehaviour
    {
        [Header("Attack Settings")]
        [SerializeField] private float startStrength = 1f;
        
        [Header("Events")]
        [SerializeField] private FloatEvent onStrengthChanged = new();
        
        private float _currentStrength;
        
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