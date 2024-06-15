using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// Bool-typed <see cref="Bool"/> to use when there is the need for passing through Bool values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Bool Event", order = 0)]
    public class BoolGameEvent : GameEventBase<bool>
    {
    }
}