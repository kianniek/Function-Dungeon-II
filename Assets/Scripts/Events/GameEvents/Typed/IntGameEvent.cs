using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// Int-typed <see cref="GameEvent"/> to use when there is the need for passing through int values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Int Event", order = 0)]
    public class IntGameEvent : GameEventBase<int>
    {
    }
}