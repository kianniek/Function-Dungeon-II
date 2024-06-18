using UI.GraphDrawer;
using UnityEngine;
using UnityEngine.Events;

namespace Towers.Configuration.UI
{
    public class TypedTowerUIController : MonoBehaviour
    {
        [Header("Grid")]
        [SerializeField] protected FunctionGraphDrawerController gridDrawer;
        [SerializeField] protected Transform gridOrigin;
        
        [Header("Events")]
        [SerializeField] protected UnityEvent onTowerConfigured = new();
        
        private TowerConfigurator _activeTower;

        internal TowerConfigurator ActiveTower
        {
            get => _activeTower;
            set
            {
                _activeTower = value;
                
                var position = _activeTower.transform.position;

                position.y += 1f;
                
                gridOrigin.position = position;
                gridDrawer.gameObject.SetActive(true);
            }
        }
    }
}