using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// Float-typed <see cref="GameEvent"/> to use when there is the need for passing through float values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Float Event", order = 0)]
    public class FloatGameEvent : GameEventBase<float>
    {
    }
}