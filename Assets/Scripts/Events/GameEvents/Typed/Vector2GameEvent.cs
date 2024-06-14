using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// Vector2-typed <see cref="GameEvent"/> to use when there is the need for passing through Vector2 values <see cref="Item"/>.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Vector2 Event", order = 0)]
    public class Vector2GameEvent : GameEventBase<Vector2>
    {
    }
}
