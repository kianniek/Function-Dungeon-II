using Events.GameEvents.Typed;
using TypedUnityEvent;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// Int-typed <see cref="GameEventListener"/> to use when there is the need for passing through int values.
    /// </summary>
    public class IntGameEventListener : GameEventListenerBase<int, IntGameEvent, IntEvent>
    {
    }
}