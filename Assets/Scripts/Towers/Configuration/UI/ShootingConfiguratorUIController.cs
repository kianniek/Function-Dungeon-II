using LinearProjectiles;
using UnityEngine;

namespace Towers.Configuration.UI
{
    public class ShootingConfiguratorUIController : MonoBehaviour
    {
        internal TowerConfigurator ActiveTower { private get; set; }
        
        public float A { private get; set; }
        
        public void OnConfirmAngle()
        {
            var shootingBehaviour = ActiveTower.GetComponent<LinearProjectileTower>();
            
            shootingBehaviour.SetShootingDirection(A);
            
            gameObject.SetActive(false);
        }
    }
}