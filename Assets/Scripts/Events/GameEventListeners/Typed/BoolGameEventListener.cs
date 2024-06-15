using Events.GameEvents.Typed;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// Bool-typed <see cref="GameEventListener"/> to use when there is the need for passing through Bool values.
    /// </summary>
    public class BoolGameEventListener : GameEventListenerBase<bool, BoolGameEvent, BoolEvent>
    {
    }
}