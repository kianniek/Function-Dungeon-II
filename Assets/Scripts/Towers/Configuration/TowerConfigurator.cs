using Attributes;
using Towers.Configuration.Events;
using UnityEngine;

namespace Towers.Configuration
{
    public class TowerConfigurator : MonoBehaviour
    {
        [SerializeField] private TowerConfigurationGameEvent onConfigureTower;
        [SerializeField, Expandable] private TowerVariables towerVariables;
        
        public TowerVariables TowerVariables => towerVariables;
        
        private void Start()
        {
            onConfigureTower?.Invoke(this);
        }

        public void ConfigureTower()
        {
            onConfigureTower?.Invoke(this);
        }
    }
}