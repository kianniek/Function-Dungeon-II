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
        [Header("Guides")]
        [SerializeField] private Transform guide;
        
        [Header("Variables")]
        [SerializeField, Expandable] private TowerVariables towerVariables;
        
        [Header("Events")]
        [SerializeField] private TowerConfigurationGameEvent onConfigureTower;
        
        /// <summary>
        /// The tower variables.
        /// </summary>
        public TowerVariables TowerVariables => towerVariables;
        
        private void Start()
        {
            onConfigureTower?.Invoke(this);

            var originalScale = guide.localScale;

            originalScale.x = towerVariables.FireRange * 2f;
            originalScale.y = towerVariables.FireRange * 2f;
            
            guide.localScale = originalScale;
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