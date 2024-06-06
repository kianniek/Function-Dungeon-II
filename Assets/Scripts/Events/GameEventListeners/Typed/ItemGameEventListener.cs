using Events.GameEvents.Typed;
using MaterialSystem;

namespace Events.GameEventListeners.Typed
{
    /// <summary>
    /// Int-typed <see cref="GameEventListener"/> to use when there is the need for passing through int values.
    /// </summary>
    public class ItemGameEventListener : GameEventListenerBase<Item, ItemGameEvent, ItemEvent>
    {
    }
}