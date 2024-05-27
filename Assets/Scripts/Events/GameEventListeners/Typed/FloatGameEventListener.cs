using Events.GameEvents.Typed;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// Float-typed <see cref="GameEventListener"/> to use when there is the need for passing through float values.
    /// </summary>
    public class FloatGameEventListener : GameEventListenerBase<float, FloatGameEvent, FloatEvent>
    {
    }
}