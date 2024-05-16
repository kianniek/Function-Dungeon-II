using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Attributes;
using Projectile;
using UnityEngine.Serialization;
using TypedUnityEvent;

namespace Cannon
{
    public class CannonProjectileController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject shootPosition;
        
        [Header("Pooling")] 
        [SerializeField, Expandable] private ProjectileScript prefabToPool;
        [SerializeField] private int amountToPool = 20;
        [SerializeField] private GameObjectEvent onCannonFire = new();

        [Header("Settings")]
        [Tooltip("The total amount of times the cannon can shoot in the current level")]
        [SerializeField] private int totalAmmo = 3;

        public int CurrentAmmoCount
        {
            get;
            private set;
        }
        
        private readonly List<ProjectileScript> _pooledProjectiles = new();
        

        private void Start()
        {
            CreatePooledProjectiles();
            CurrentAmmoCount = totalAmmo;
        }
        
        /// <summary>
        /// Empties the current pooled object list and creates a new pool
        /// </summary>
        private void CreatePooledProjectiles()
        {
            // if (prefabToPool == null)
            // {
            //     Debug.LogWarning($"Prefab {prefabToPool.name} doesn't have a ProjectileScript!");
            //     
            //     return;
            // }
            
            // Checks if there are already pooled objects and destroys existing pooled gameObjects
            if (_pooledProjectiles.Count > 0)
            {
                foreach (var projectile in _pooledProjectiles)
                {
                    Destroy(projectile.gameObject);
                }
            }
            
            // Creates a new list of pooled objects
            _pooledProjectiles.Clear();
            
            for (var i = 0; i < amountToPool; i++)
            {
                var projectile = Instantiate(prefabToPool, transform);
                
                projectile.gameObject.SetActive(false);
                
                _pooledProjectiles.Add(projectile);
            }
        }
        
        /// <summary>
        /// Returns a pooled projectile if available, otherwise returns null
        /// </summary>
        private ProjectileScript GetPooledProjectile()
        {
            for (var i = 0; i < amountToPool; i++)
            {
                if (!_pooledProjectiles[i].gameObject.activeInHierarchy)
                {
                    return _pooledProjectiles[i];
                }
            }
            
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