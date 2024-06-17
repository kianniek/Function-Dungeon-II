using Events.GameEvents;
using UnityEngine;

namespace Towers.Configuration.Events
{
    /// <summary>
    /// A game event that passes a TowerConfigurator.
    /// </summary>
    [CreateAssetMenu(menuName = "Game Events/Tower Configuration Game Event")]
    public class TowerConfigurationGameEvent : GameEventBase<TowerConfigurator>
    {
    }
}