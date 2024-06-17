using LinearProjectiles;
using UnityEngine;
using Utils;

namespace Towers.Configuration.UI
{
    public class BombingConfiguratorUIController : TypedTowerUIController
    {
        public string X { private get; set; }
 
        public string Y { private get; set; }

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