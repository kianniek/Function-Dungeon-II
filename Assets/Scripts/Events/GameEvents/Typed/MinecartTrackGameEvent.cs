using MineCart;
using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// GameObject-typed <see cref="GameEvent"/> to use when there is the need for passing through GameObject values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/MineCartTrack Event", order = 0)]
    public class MineCartTrackGameEvent : GameEventBase<MineCartTrack>
    {
    }
}