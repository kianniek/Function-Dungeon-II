using LinearProjectiles;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Towers.Configuration.UI
{
    /// <summary>
    /// A UI controller for configuring a bombing tower.
    /// </summary>
    public class BombingConfiguratorUIController : TypedTowerUIController
    {
        [Header("Configuration Guide")]
        [SerializeField] private Image bombingPositionGuide;
        
        private float _x;
        private float _y;
        private Vector3 _bombingPosition;

        private void Start()
        {
            _x = 0;
            _y = 0;
            
            UpdatePosition();
        }

        /// <summary>
        /// The x coordinate of the bombing position.
        /// </summary>
        public string X
        {
            private get => $"{_x}";
            set
            {
                _x = string.IsNullOrEmpty(value) ? 0 : float.Parse(StringExtensions.CleanUpDecimalOnlyString(value));
                    
                UpdatePosition();
            }
        }

        /// <summary>
        /// The y coordinate of the bombing position.
        /// </summary>
        public string Y
        {
            private get => $"{_y}";
            set
            {
                _y = string.IsNullOrEmpty(value) ? 0 : float.Parse(StringExtensions.CleanUpDecimalOnlyString(value));
                
                UpdatePosition();
            }
        }

        /// <summary>
        /// Called when the user confirms the coordinates.
        /// </summary>
        public override void OnConfirmButtonClicked()
        {
            if (!BombPositionInRange())
                return;
            
            ActiveTower.GetComponent<SimpleProjectileLauncher>().SetShootingPosition(_bombingPosition);
            
            onTowerConfigured?.Invoke();
            
            gameObject.SetActive(false);
        }

        private void UpdatePosition()
        {
            if (UnityEngine.Camera.main is not { } mainCamera) 
                return;
            
            _bombingPosition = new Vector3
            {
                x = ActiveTower.transform.position.x + _x,
                y = ActiveTower.transform.position.y,
                z = ActiveTower.transform.position.z + _y
            };
            
            bombingPositionGuide.rectTransform.position = mainCamera.WorldToScreenPoint(_bombingPosition);
            bombingPositionGuide.color = BombPositionInRange() ? Color.green : Color.red;
        }

        private bool BombPositionInRange()
        {
            var distance = _bombingPosition.Distance(ActiveTower.transform.position);
            
            return distance <= ActiveTower.TowerVariables.FireRange && distance >= TowerVariables.MinFireRange;
        }
    }
}