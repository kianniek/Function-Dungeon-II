using System.Collections;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Towers
{
    public class BombTower : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private GameObjectGameEvent onBombTowerPlaced;
        [SerializeField] private Vector2GameEvent bombCoordinatesSet;
        [SerializeField] private Vector2GameEvent onBombShooted;

        [Header("Variables")]
        [SerializeField] private TowerVariables bombTowerVariables;

        [Header("Sprites")]
        [SerializeField] private GameObject rangeCircle;

        private Vector2 _bombPosition;
        private bool _isAttacking;

        private void Start()
        {
            onBombTowerPlaced.Invoke(gameObject);
            bombCoordinatesSet.AddListener(BombCoordinatesSet);
        }

        /// <summary>
        /// Sets bombcoordinate from event
        /// </summary>
        /// <param name="bombPosition">The bomb position</param>
        private void BombCoordinatesSet(Vector2 bombPosition)
        {
            _bombPosition = bombPosition;
            rangeCircle.SetActive(false);
            bombCoordinatesSet.RemoveListener(BombCoordinatesSet);
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
            yield return new WaitForSeconds(bombTowerVariables.ShootingSpeed);
            onBombShooted.Invoke(_bombPosition);
            _isAttacking = false;
        }
    }
}
