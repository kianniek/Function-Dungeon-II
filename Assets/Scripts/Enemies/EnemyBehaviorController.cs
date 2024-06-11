using System.Collections;
using Health;
using UnityEngine;
using UnityEngine.AI;
using WorldGrid;

namespace Enemies
{
    /// <summary>
    /// Class which handles enemy movement and atack
    /// </summary>
    public class EnemyBehaviorController : MonoBehaviour
    {
        [SerializeField] private GridData gridData;
        [SerializeField] private int movementSpeed;
        [SerializeField] private int damage;
        [SerializeField] private int attackSpeed;

        private int _gridTileWidth = 1;
        private int _gridTileHeight = 1;

        private float _enemyTowerRadius = 1f;
        private int _currentTargetIndex;
        private bool _isAttacking;
        private Vector3 _targetPosition;

        private void Start()
        {
            transform.position = new Vector3(gridData.PathIndices[0].x * _gridTileWidth, gridData.PathIndices[0].y * _gridTileHeight, transform.position.z);
        }

        /// <summary>
        /// Moves enemy and checks if enemy is on next waypoint. If the enemy is on the waypoint check for nearby towers and add an index if there are no towers
        /// </summary>
        private void FixedUpdate()
        {
            _targetPosition = new Vector3(gridData.PathIndices[_currentTargetIndex].x, gridData.PathIndices[_currentTargetIndex].y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, movementSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _targetPosition) > 0.01f || _currentTargetIndex == gridData.PathIndices.Count - 1)
                return;

            if (ClosestTower() != null)
            {
                if (_isAttacking)
                    return;

                StartCoroutine(AttackCoroutine(ClosestTower()));
            }
            else
            {
                _currentTargetIndex = _currentTargetIndex + 1;
            }

        }

        /// <summary>
        /// Find closest tower for enemy
        /// </summary>
        /// <returns>Closest tower for enemy</returns>
        private Damageable ClosestTower()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, _enemyTowerRadius);
            foreach (Collider hit in hits)
            {
                //Filter enemies (and possibly later more) out
                if (!hit.TryGetComponent<EnemyBehaviorController>(out _))
                {
                    return hit.gameObject.GetComponent<Damageable>();
                }
            }
            return null;
        }

        /// <summary>
        /// Handles enemy attack and dealing damage to a tower
        /// </summary>
        /// <param name="tower">Tower to damage</param>
        private IEnumerator AttackCoroutine(Damageable tower)
        {
            _isAttacking = true;
            while (ClosestTower() == tower)
            {
                yield return new WaitForSeconds(attackSpeed);
                tower.Health -= damage;
                _isAttacking = false;
                break;
            }
        }
    }
}
