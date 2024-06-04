using System.Collections;
using System.Collections.Generic;
using Health;
using UnityEngine;
using WorldGrid;

public class EnemyBehaviorController : MonoBehaviour
{
    [SerializeField] private PathData pathData;
    [SerializeField] private int movementSpeed;
    [SerializeField] private int damage;
    [SerializeField] private int attackSpeed;

    private float _enemyTowerRadius = 1f;
    private int _currentTargetIndex;

    private void Start()
    {
        transform.position = pathData.PathCoordinates[0];
    }

    private void FixedUpdate()
    {
        //Code for moving along path
        Vector3 targetPosition = pathData.PathCoordinates[_currentTargetIndex];
        transform.position = Vector3.MoveTowards(transform.position, pathData.PathCoordinates[_currentTargetIndex], movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f && _currentTargetIndex != pathData.PathCoordinates.Count - 1)
        {
            if (ClosestTower() != null)
            {
                StartCoroutine(AttackCoroutine(ClosestTower()));
                return;
            }
            else
            {
                _currentTargetIndex = _currentTargetIndex + 1;
            }
        }
    }

    private Damageable ClosestTower()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _enemyTowerRadius);
        foreach (Collider hit in hits)
        {
            if (!hit.GetComponent<EnemyBehaviorController>())
            {
                return hit.gameObject.GetComponent<Damageable>();
            }
        }
        return null;
    }
    private IEnumerator AttackCoroutine(Damageable tower)
    {
        Debug.Log(tower.gameObject.name);
        yield return new WaitForSeconds(attackSpeed);
        tower.Health -= damage;
    }
}
