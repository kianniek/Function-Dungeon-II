using Progression.Grading;
using UnityEngine;

namespace GameEvent.Events.Typed
{
    /// <summary>
    /// Float-typed <see cref="GameEvent"/> to use when there is the need for passing through float values.
    /// </summary>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Grade Event", order = 0)]
    public class GradeGameEvent : GameEventBase<Grade>
    {
    }
}