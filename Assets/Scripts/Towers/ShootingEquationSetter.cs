using Events.GameEvents.Typed;
using UI.LinearEquation;
using UnityEngine;

namespace Towers
{
    public class ShootingEquationSetter : MonoBehaviour
    {
        [Header("Tower variables")]
        [SerializeField] private TowerVariables shootingTowerVariables;

        [Header("GUI References")]
        [SerializeField] private LinearEquationTextModifier linearEquationTextModifier;

        [Header("Events")]
        [SerializeField] private Vector2GameEvent bulletCoordinatesSet;
        [SerializeField] private GameObjectGameEvent onShootingTowerPlaced;

        private GameObject _shootingTower;

        private void Awake()
        {
            onShootingTowerPlaced.AddListener(SetShootingTower);
        }
        
        private void SetShootingTower(GameObject shootingTower)
        {
            _shootingTower = shootingTower;
        }

        /// <summary>
        /// Translates player input formula to a point within tower range. Bullets will be shooted towards this point.
        /// Invoke the event to send coordinates back to tower
        /// </summary>
        public void OnConfirmButtonClicked()
        {
            var startPoint = new Vector2(_shootingTower.transform.position.x, _shootingTower.transform.position.y);
            var a = linearEquationTextModifier.AVariable;
            var direction = new Vector2(1, a);
            var normalizedDirection = direction.normalized;
            var scaledDirection = normalizedDirection * shootingTowerVariables.FireRange;
            var endpoint = startPoint + scaledDirection;

            bulletCoordinatesSet.Invoke(endpoint);
        }
    }
}