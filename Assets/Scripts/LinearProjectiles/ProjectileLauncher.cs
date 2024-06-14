using Attributes;
using Events;
using Events.GameEvents.Typed;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace LinearProjectiles
{
    public class ProjectileLauncher : MonoBehaviour
    {
        [Header("Projectiles")] 
        [SerializeField, Expandable] private ProjectileScript prefabToPool;
        [SerializeField] private int amountToPool = 20;
        
        [Header("References")] 
        [SerializeField] private GameObject shootPosition;
        
        [Header("Settings")]
        [Tooltip("The total amount of times the cannon can shoot in the current level")]
        [SerializeField] private int totalAmmo = 3;
        
        [Header("Events")] 
        [SerializeField] private IntGameEvent onAmmoChange;
        [SerializeField] private GameObjectEvent onCannonFire = new();
        [SerializeField] private UnityEvent onAmmoDepleted = new();
        
        private ObjectPool<ProjectileScript> _projectilePool;
        private int _currentAmmoCount;
        
        private int CurrentAmmoCount
        {
            get => _currentAmmoCount;
            set
            {
                if (value < 0 )
                    return;
                
                _currentAmmoCount = value;
                
                if (value == 0)
                    onAmmoDepleted.Invoke();
                
                onAmmoChange?.Invoke(value);
            }
        }
        
        private void Awake()
        {
            _projectilePool = new ObjectPool<ProjectileScript>(prefabToPool, amountToPool);
        }
        
        private void Start()
        {
            CurrentAmmoCount = totalAmmo;
        }
        
        /// <summary>
        /// Activates a pooled projectile and invokes the cannon fired event, will not work if the cannon doesn't have ammo
        /// </summary>
        public void ShootProjectile()
        {
            if (CurrentAmmoCount <= 0)
                return;
            
            CurrentAmmoCount--;
            
            var projectile = _projectilePool.GetPooledObject();
            
            if (projectile)
            {
                projectile.transform.position = transform.position;
                projectile.gameObject.SetActive(true);
                projectile.Shoot(shootPosition.transform.rotation);
                
                onCannonFire.Invoke(projectile.gameObject);
            }
            else
            {
                Debug.LogWarning(
                    $"Not enough pooled projectiles! Consider increasing the current amount: {amountToPool}");
            }
        }
    }
}