using Events.GameEvents.Typed;
using TypedUnityEvent;


namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// GameObject-typed <see cref="GameEventListener"/> to use when there is the need for passing through GameObject values.
    /// </summary>
    public class GameObjectGameEventListener : GameEventListenerBase<UnityEngine.GameObject, GameObjectGameEvent, GameObjectEvent>
    {
    }
}