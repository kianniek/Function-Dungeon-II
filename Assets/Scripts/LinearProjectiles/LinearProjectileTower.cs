using System;
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
        private Vector3 _shootingPosition;
        
        private void Awake()
        {
            _towerConfigurator = GetComponent<TowerConfigurator>();
            _projectilePool = new SimpleObjectPool<LinearProjectile>(projectilePrefab, 30, transform);
        }

        public void SetShootingPosition(float x, float y)
        {
            _shootingPosition = new Vector3(x, 1.1f, y);
        }

        public void SetShootingDirection(float a)
        {
            var x = MathF.Cos(a * Mathf.Deg2Rad);
            var y = MathF.Sin(a * Mathf.Deg2Rad);
            
            SetShootingPosition(x, y);
        }
    }
}