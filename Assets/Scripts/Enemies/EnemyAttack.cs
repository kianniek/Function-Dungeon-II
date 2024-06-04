using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int attackSpeed;

    private void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackSpeed);

        //TODO attack
    }
}

