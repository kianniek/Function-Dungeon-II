using Events.GameEvents.Typed;
using UI;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// ExtendedButton-typed <see cref="GameEventListener"/> to use when there is the need for passing through ExtendedButton values.
    /// </summary>
    public class ExtendedButtonGameEventListener : GameEventListenerBase<ExtendedButton, ExtendedButtonGameEvent, ExtendedButtonEvent>
    {
    }
}