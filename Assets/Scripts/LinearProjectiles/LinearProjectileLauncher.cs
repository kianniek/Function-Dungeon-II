using System.Collections.Generic;
using Attributes;
using Events.GameEvents.Typed;
using UnityEngine;
using UnityEngine.Events;

namespace LinearProjectiles
{
    /// <summary>
    /// This class is responsible for managing the linear projectile launcher and its behavior.
    /// </summary>
    public class LinearProjectileLauncher : MonoBehaviour
    {
        private readonly List<LinearProjectile> _pooledProjectiles = new();
        
        [Header("Projectiles")] 
        [SerializeField, Expandable] private LinearProjectile prefabToPool;
        [SerializeField] private int amountToPool = 20;
        
        [Header("References")] 
        [SerializeField] private GameObject shootPosition;
        
        [Header("Settings")]
        [Tooltip("The total amount of times the cannon can shoot in the current level")]
        [SerializeField] private int totalAmmo = 3;
        
        [Header("Events")] 
        [SerializeField] private IntGameEvent onAmmoChange;
        [SerializeField] private UnityEvent onAmmoDepleted = new();
        [SerializeField] private LinearProjectileEvent onShootProjectile = new();
        
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
        
        private void Start()
        {
            CreatePooledProjectiles();
            CurrentAmmoCount = totalAmmo;
        }
        
        // Empties the current pooled object list and creates a new pool
        private void CreatePooledProjectiles()
        {
            // Checks if there are already pooled objects and destroys existing pooled gameObjects
            if (_pooledProjectiles.Count > 0)
                foreach (var projectile in _pooledProjectiles) 
                    Destroy(projectile.gameObject);
            
            _pooledProjectiles.Clear();
            
            for (var i = 0; i < amountToPool; i++)
            {
                var projectile = Instantiate(prefabToPool, transform);
                
                projectile.gameObject.SetActive(false);
                
                _pooledProjectiles.Add(projectile);
            }
        }
        
        // Returns a pooled projectile if available, otherwise returns null
        private LinearProjectile GetPooledProjectile()
        {
            for (var i = 0; i < amountToPool; i++)
                if (!_pooledProjectiles[i].gameObject.activeInHierarchy)
                    return _pooledProjectiles[i];
            
            return null;
        }
        
        /// <summary>
        /// Activates a pooled projectile and invokes the cannon fired event, will not work if the cannon doesn't have ammo
        /// </summary>
        public void ShootProjectile()
        {
            if (CurrentAmmoCount <= 0)
                return;
            
            CurrentAmmoCount--;
            
            var projectile = GetPooledProjectile();
            
            if (projectile)
            {
                projectile.transform.position = transform.position;
                projectile.gameObject.SetActive(true);
                projectile.Shoot(shootPosition.transform.rotation);
                
                onShootProjectile.Invoke(projectile);
            }
            else
            {
                Debug.LogWarning(
                    $"Not enough pooled projectiles! Consider increasing the current amount: {amountToPool}");
            }
        }
    }
}