using System.Collections;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Towers
{
    public class ShootTower : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private GameObjectGameEvent onShootingTowerPlaced;
        [SerializeField] private Vector2GameEvent onBulletShooted;

        [Header("Variables")]
        [SerializeField] private TowerVariables shootingTowerVariables;

        private Vector2 _bulletPosition;
        private bool _isAttacking;

        private void Start()
        {
            onShootingTowerPlaced.Invoke(gameObject);
        }
        private void FixedUpdate()
        {
            if (_isAttacking)
                return;

            StartCoroutine(AttackCoroutine());
        }
        private IEnumerator AttackCoroutine()
        {
            _isAttacking = true;
            yield return new WaitForSeconds(shootingTowerVariables.ShootingSpeed);
            onBulletShooted.Invoke(_bulletPosition);
            _isAttacking = false;
        }
    }
}
