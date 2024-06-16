using LinearProjectiles;
using UnityEngine;
using Utils;

namespace Towers.Configuration.UI
{
    public class BombingConfiguratorUIController : MonoBehaviour
    {
        internal TowerConfigurator ActiveTower { private get; set; }

        public string X { private get; set; }
 
        public string Y { private get; set; }

        public void OnConfirmCoordinates()
        {
            var shootingBehaviour = ActiveTower.GetComponent<LinearProjectileTower>();
            
            Debug.Log($"X: {X}, Y: {Y}");

            if (string.IsNullOrEmpty(X) || string.IsNullOrEmpty(Y))
                return;
            
            var answer = new Vector2(
                float.Parse(StringExtensions.CleanUpDecimalOnlyString(X)),
                float.Parse(StringExtensions.CleanUpDecimalOnlyString(Y))
            );
            
            Debug.Log($"X: {answer.x}, Y: {answer.y}");

            if (
                answer.x < ActiveTower.transform.position.x - ActiveTower.TowerVariables.FireRange ||
                answer.x > ActiveTower.transform.position.x + ActiveTower.TowerVariables.FireRange ||
                answer.y < ActiveTower.transform.position.x - ActiveTower.TowerVariables.FireRange ||
                answer.y > ActiveTower.transform.position.x + ActiveTower.TowerVariables.FireRange
            )
                return;
            
            Debug.Log("Coordinates are in range");
            
            shootingBehaviour.SetShootingPosition(answer.x, answer.y);
            
            gameObject.SetActive(false);
        }
    }
}