using LinearProjectiles;
using UnityEngine;

namespace Towers.Configuration.UI
{
    public class ShootingConfiguratorUIController : TypedTowerUIController
    {
        public float A { private get; set; }
        
        public void OnConfirmAngle()
        {
            var shootingBehaviour = ActiveTower.GetComponent<LinearProjectileTower>();
            
            shootingBehaviour.SetShootingDirection(A);
            
            onTowerConfigured?.Invoke();
            
            gameObject.SetActive(false);
        }
    }
}