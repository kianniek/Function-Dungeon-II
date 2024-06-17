using Attributes;
using Towers.Configuration.Events;
using UnityEngine;

namespace Towers.Configuration
{
    /// <summary>
    /// A component that configures a tower.
    /// </summary>
    public class TowerConfigurator : MonoBehaviour
    {
        [SerializeField] private TowerConfigurationGameEvent onConfigureTower;
        [SerializeField, Expandable] private TowerVariables towerVariables;
        
        /// <summary>
        /// The tower variables.
        /// </summary>
        public TowerVariables TowerVariables => towerVariables;
        
        private void Start()
        {
            onConfigureTower?.Invoke(this);
        }

        /// <summary>
        /// Configures the tower.
        /// </summary>
        public void ConfigureTower()
        {
            onConfigureTower?.Invoke(this);
        }
    }
}