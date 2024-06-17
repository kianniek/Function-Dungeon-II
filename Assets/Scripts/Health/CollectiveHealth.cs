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

        public Damageable Damageable { get; private set; }

        private void Awake()
        {
            Damageable = GetComponent<Damageable>();
        }
        
        private void OnEnable()
        {
            collectiveHealthContainer?.AddDamageable(Damageable);
        }
        
        private void OnDisable()
        {
            collectiveHealthContainer?.RemoveDamageable(Damageable);
        }
    }
}