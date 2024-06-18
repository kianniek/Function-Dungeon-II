using System;
using Attributes;
using Events.GameEvents;
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
        [SerializeField] private GameEvent onWaveStart;
        [SerializeField] private GameEvent onEnterBuildMode;
        [SerializeField] private TowerConfigurationGameEvent onConfigureTower;
        
        /// <summary>
        /// The tower variables.
        /// </summary>
        public TowerVariables TowerVariables => towerVariables;

        private void OnEnable()
        {
            onWaveStart?.AddListener(DisableGuide);
            onEnterBuildMode?.AddListener(EnableGuide);
        }
        
        private void OnDisable()
        {
            onWaveStart?.RemoveListener(DisableGuide);
            onEnterBuildMode?.RemoveListener(EnableGuide);
        }

        private void Start()
        {
            onConfigureTower?.Invoke(this);

            var originalScale = guide.localScale;

            originalScale.x = towerVariables.FireRange * 2f / transform.localScale.x;
            originalScale.y = towerVariables.FireRange * 2f / transform.localScale.y;
            
            guide.localScale = originalScale;
        }
        
        private void EnableGuide()
        {
            guide.gameObject.SetActive(true);
        }
        
        private void DisableGuide()
        {
            guide.gameObject.SetActive(false);
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