using UnityEngine;

namespace Health
{
    /// <summary>
    /// Adds the damageable to a health container on enable and removes it on disable,
    /// allowing for the health to be collocated in a single place.
    /// </summary>
    [RequireComponent(typeof(Damageable))]
    public class CollectiveHealth : MonoBehaviour
    {
        [SerializeField] private CollectiveHealthContainer collectiveHealthContainer;
        
        private Damageable _damageable;
        
        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
        }
        
        private void OnEnable()
        {
            collectiveHealthContainer.AddDamageable(_damageable);
        }
        
        private void OnDisable()
        {
            collectiveHealthContainer.RemoveDamageable(_damageable);
        }
    }
}