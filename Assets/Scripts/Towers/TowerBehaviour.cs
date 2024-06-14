using System.Collections;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Towers
{
    public class TowerBehaviour : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private GameObjectGameEvent onTowerPlaced;
        [SerializeField] private Vector2GameEvent coordinatesSet;
        [SerializeField] private Vector2GameEvent onProjectileShot;

        [Header("Variables")]
        [SerializeField] private TowerVariables towerVariables;

        [Header("Sprites")]
        [SerializeField] private GameObject rangeCircle;

        private Vector2 _projectilePosition;
        private bool _isAttacking;

        private void Start()
        {
            onTowerPlaced.Invoke(gameObject);
            coordinatesSet.AddListener(BombCoordinatesSet);
        }

        /// <summary>
        /// Sets projectile coordinate from event
        /// </summary>
        /// <param name="projectilePosition">The projectile position</param>
        private void BombCoordinatesSet(Vector2 projectilePosition)
        {
            _projectilePosition = projectilePosition;
            rangeCircle.SetActive(false);
            coordinatesSet.RemoveListener(BombCoordinatesSet);
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
            yield return new WaitForSeconds(towerVariables.ShootingSpeed);
            onProjectileShot.Invoke(_projectilePosition);
            _isAttacking = false;
        }
    }
}
