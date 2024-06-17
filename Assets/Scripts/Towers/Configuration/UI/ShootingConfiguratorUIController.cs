using LinearProjectiles;
using UnityEngine;

namespace Towers.Configuration.UI
{
    /// <summary>
    /// A UI controller for configuring a shooting tower.
    /// </summary>
    public class ShootingConfiguratorUIController : TypedTowerUIController
    {
        /// <summary>
        /// The angle of the shooting direction.
        /// </summary>
        public float A { private get; set; }
        
        /// <summary>
        /// Called when the user confirms the angle.
        /// </summary>
        public void OnConfirmAngle()
        {
            var shootingBehaviour = ActiveTower.GetComponent<LinearProjectileTower>();
            
            shootingBehaviour.SetShootingDirection(A);
            
            onTowerConfigured?.Invoke();
            
            gameObject.SetActive(false);
        }
    }
}