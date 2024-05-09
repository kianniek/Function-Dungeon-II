using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Attributes;
using Projectile;
using UnityEngine.Serialization;

namespace Cannon
{
    public class CannonProjectileController : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private GameObject shootPosition;
        
        [Header("Pooling")] 
        [SerializeField, Expandable] private ProjectileScript prefabToPool;
        [SerializeField] private int amountToPool = 20;
        [SerializeField] private UnityEvent onCannonFire = new();
        
        private readonly List<ProjectileScript> _pooledProjectiles = new();
        
        // Start is called before the first frame update
        private void Start()
        {
            CreatePooledProjectiles();
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
        /// Activates a pooled projectile and invokes the cannon fired event 
        /// </summary>
        public void ShootProjectile()
        {
            var projectile = GetPooledProjectile();
            
            if (projectile)
            {
                projectile.transform.position = transform.position;
                projectile.gameObject.SetActive(true);
                
                projectile.Shoot(shootPosition.transform.rotation);
                
                onCannonFire.Invoke();
            }
            else
            {
                Debug.LogWarning(
                    $"Not enough pooled projectiles! Consider increasing the current amount: {amountToPool}");
            }
        }
    }
}