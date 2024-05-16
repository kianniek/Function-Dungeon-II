using GameEvent.Listeners;

namespace Progression.Grading
{
    /// <summary>
    /// Grade-typed <see cref="GameEventListener"/> to use when there is the need for passing through grade values.
    /// </summary>
    public class GradeGameEventListener : GameEventListenerBase<Grade, GradeGameEvent, GradeEvent>
    {
    }
}