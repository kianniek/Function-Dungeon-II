using Events.GameEvents.Typed;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// Button-typed <see cref="GameEventListener"/> to use when there is the need for passing through Button values.
    /// </summary>
    public class ButtonGameEventListener : GameEventListenerBase<UnityEngine.UI.Button, ButtonGameEvent, ButtonEvent>
    {
    }
}