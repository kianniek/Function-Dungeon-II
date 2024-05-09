using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Projectile;

namespace Cannon
{
    public class CannonProjectileController : MonoBehaviour
    {
        [SerializeField] private GameObject shootPosition;

        [SerializeField] private GameObject prefabToPool;

        [SerializeField] private int amountToPool = 20;

        [SerializeField] private UnityEvent OnCannonFire = new();

        private List<ProjectileScript> _pooledProjectiles = new();

        // Start is called before the first frame update
        private void Start()
        {
            CreatePooledProjectiles();
        }

        private void OnValidate()
        {
            if (Application.isPlaying)
                CreatePooledProjectiles();
        }


        /// <summary>
        /// Empties the current pooled object list and creates a new pool
        /// </summary>
        private void CreatePooledProjectiles()
        {
            if (prefabToPool.GetComponent<ProjectileScript>() == null)
            {
                Debug.LogWarning($"Prefab {prefabToPool.name} doesn't have a ProjectileScript!");
                return;
            }

            // Checks if there are already pooled objects and destroys exisitng pooled gameObjects
            if (_pooledProjectiles != null && _pooledProjectiles.Count > 0)
            {
                for (var i = 0; i < _pooledProjectiles.Count; i++)
                {
                    Destroy(_pooledProjectiles[i].gameObject);
                }
            }

            // Creates a new list of pooled objects
            _pooledProjectiles = new();
            GameObject tmp;
            for (var i = 0; i < amountToPool; i++)
            {
                tmp = Instantiate(prefabToPool);
                tmp.SetActive(false);
                _pooledProjectiles.Add(tmp.GetComponent<ProjectileScript>());
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
            ProjectileScript projectile = GetPooledProjectile();
            if (projectile)
            {
                projectile.transform.position = transform.position;
                projectile.gameObject.SetActive(true);

                projectile.Shoot(shootPosition.transform.rotation);

                OnCannonFire.Invoke();
            }
            else
            {
                Debug.LogWarning($"Not enough pooled projectiles! Consider increasing the current amount: {amountToPool}");
            }
        }
    }
}