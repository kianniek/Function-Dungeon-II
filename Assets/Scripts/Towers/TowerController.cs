using System.Collections;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Towers
{
    public class TowerController : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private TowerVariablesGameEvent onTowerPlaced;
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
            onTowerPlaced?.Invoke(towerVariables);
            coordinatesSet.AddListener(BombCoordinatesSet);
        }
        
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
            
            yield return new WaitForSeconds(towerVariables.FireRate);
            
            onProjectileShot.Invoke(_projectilePosition);
            
            _isAttacking = false;
        }
    }
}
