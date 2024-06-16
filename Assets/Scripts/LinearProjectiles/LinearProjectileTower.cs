using Towers.Configuration;
using UnityEngine;
using Utils;

namespace LinearProjectiles
{
    [RequireComponent(typeof(TowerConfigurator))]
    public class LinearProjectileTower : MonoBehaviour
    {
        [SerializeField] private LinearProjectile projectilePrefab;
        
        private SimpleObjectPool<LinearProjectile> _projectilePool;
        private TowerConfigurator _towerConfigurator;
        
        private void Awake()
        {
            _towerConfigurator = GetComponent<TowerConfigurator>();
            _projectilePool = new SimpleObjectPool<LinearProjectile>(projectilePrefab, 10);
        }

        public void SetShootingPosition(float x, float y)
        {
            
        }

        public void SetShootingDirection(float a)
        {
            
        }
    }
}