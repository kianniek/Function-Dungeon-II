using UnityEngine;
using Utils;

namespace LinearProjectiles
{
    public class LinearProjectileTower : MonoBehaviour
    {
        [SerializeField] private LinearProjectile projectilePrefab;
        
        private SimpleObjectPool<LinearProjectile> _projectilePool;
        
        private void Awake()
        {
            _projectilePool = new SimpleObjectPool<LinearProjectile>(projectilePrefab, 10);
        }
    }
}