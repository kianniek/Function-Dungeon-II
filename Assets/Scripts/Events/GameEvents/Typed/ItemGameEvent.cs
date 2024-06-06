using MaterialSystem;
using UnityEngine;

namespace Events.GameEvents.Typed
{
    /// <summary>
    /// Item-typed <see cref="GameEvent"/> to use when there is the need for passing through Item values <see cref="Item"/>.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Item Event", order = 0)]
    public class ItemGameEvent : GameEventBase<Item>
    {
    }
}