using System;
using UnityEngine.Events;

namespace Towers.Configuration.Events
{
    /// <summary>
    /// Event that passes TowerVariables
    /// </summary>
    [Serializable]
    public class TowerConfigurationEvent : UnityEvent<TowerConfigurator>
    {
    }
}