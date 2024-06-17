using LinearProjectiles;
using UnityEngine;
using Utils;

namespace Towers.Configuration.UI
{
    /// <summary>
    /// A UI controller for configuring a bombing tower.
    /// </summary>
    public class BombingConfiguratorUIController : TypedTowerUIController
    {
        /// <summary>
        /// The x coordinate of the bombing position.
        /// </summary>
        public string X { private get; set; }
 
        /// <summary>
        /// The y coordinate of the bombing position.
        /// </summary>
        public string Y { private get; set; }

        /// <summary>
        /// Called when the user confirms the coordinates.
        /// </summary>
        public void OnConfirmCoordinates()
        {
            var shootingBehaviour = ActiveTower.GetComponent<LinearProjectileTower>();

            if (string.IsNullOrEmpty(X) || string.IsNullOrEmpty(Y))
                return;
            
            var answer = new Vector2(
                float.Parse(StringExtensions.CleanUpDecimalOnlyString(X)),
                float.Parse(StringExtensions.CleanUpDecimalOnlyString(Y))
            );
            
            if (answer.Distance(ActiveTower.transform.position) > ActiveTower.TowerVariables.FireRange)
                return;
            
            shootingBehaviour.SetShootingPosition(answer.x, answer.y);
            
            onTowerConfigured?.Invoke();
            
            gameObject.SetActive(false);
        }
    }
}