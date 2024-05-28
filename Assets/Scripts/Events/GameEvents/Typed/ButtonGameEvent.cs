using UnityEngine;
using UnityEngine.UI;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// GameObject-typed <see cref="Button"/> to use when there is the need for passing through Button values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Button Event", order = 0)]
    public class ButtonGameEvent : GameEventBase<Button>
    {
    }
}