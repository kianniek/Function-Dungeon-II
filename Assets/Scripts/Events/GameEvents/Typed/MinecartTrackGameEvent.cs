using Minecart;
using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// GameObject-typed <see cref="GameEvent"/> to use when there is the need for passing through GameObject values.
    /// </summary>
    [CreateAssetMenu(fileName = "MinecartTrack", menuName = "Game Events/MinecartTrack Event", order = 0)]
    public class MinecartTrackGameEvent : GameEventBase<MinecartTrack>
    {
    }
}