using Events.GameEvents;

namespace Events.GameEventListeners
{
    /// <summary>
    /// Script to add <see cref="IGameEvent"/> listener functionality to an object.
    /// </summary>
    public interface IGameEventListener
    {
        /// <summary>
        /// Behaviour when the <see cref="IGameEvent"/> is invoked.
        /// </summary>
        void OnInvoked();
    }
}