using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// ExtendedButton-typed <see cref="ExtendedButton"/> to use when there is the need for passing through ExtendedButton values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Extended Button Event", order = 0)]
    public class ExtendedButtonGameEvent : GameEventBase<ExtendedButton>
    {
    }
}