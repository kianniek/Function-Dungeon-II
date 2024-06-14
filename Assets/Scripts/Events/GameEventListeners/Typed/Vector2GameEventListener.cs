using Events.GameEvents.Typed;
using UnityEngine;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// Vector2-typed <see cref="GameEventListener"/> to use when there is the need for passing through Vector2 values.
    /// </summary>
    public class Vector2GameEventListener : GameEventListenerBase<Vector2, Vector2GameEvent, Vector2Event>
    {
    }
}
