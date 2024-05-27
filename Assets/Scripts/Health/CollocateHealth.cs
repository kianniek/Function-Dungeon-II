using UnityEngine;

namespace Health
{
    [RequireComponent(typeof(Damageable))]
    public class CollocateHealth : MonoBehaviour
    {
        [SerializeField] private HealthContainer healthContainer;
        
        private Damageable _damageable;
        
        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
        }
    }
}