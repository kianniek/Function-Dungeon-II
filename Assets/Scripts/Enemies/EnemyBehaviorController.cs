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
        [SerializeField] private GridGenerator gridGenerator;
        [SerializeField] private int movementSpeed;
        [SerializeField] private int damage;
        [SerializeField] private int attackSpeed;

        private NavMeshAgent _navMeshAgent;
        private float _enemyTowerRadius = 1f;
        private bool _isAttacking;
        private Vector3 _targetPosition;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            transform.position = gridGenerator.PathStartPosition;
            _navMeshAgent.SetDestination(gridGenerator.PathEndPosition);
        }

        /// <summary>
        /// Moves enemy and checks if enemy is on next waypoint. If the enemy is on the waypoint check for nearby towers and add an index if there are no towers
        /// </summary>
        private void FixedUpdate()
        {
            if (ClosestTower() == null)
                return;

            if (_isAttacking)
                return;

            StartCoroutine(AttackCoroutine(ClosestTower()));


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
