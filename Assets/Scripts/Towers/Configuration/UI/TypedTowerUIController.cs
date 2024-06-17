using UnityEngine;
using UnityEngine.Events;

namespace Towers.Configuration.UI
{
    public class TypedTowerUIController : MonoBehaviour
    {
        [SerializeField] protected UnityEvent onTowerConfigured = new();
            
        internal TowerConfigurator ActiveTower { get; set; }
    }
}