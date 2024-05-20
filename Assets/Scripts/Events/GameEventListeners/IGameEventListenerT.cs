using Events.GameEvents;

namespace Events.GameEventListeners
{
    /// <summary>
    /// Script to add <see cref="IGameEvent{T}"/> listener functionality to an object.
    /// </summary>
    /// <typeparam name="T"> Type of value to pass through the <see cref="IGameEvent{T}"/>. </typeparam>
    public interface IGameEventListener<in T>
    {
        /// <summary>
        /// Behaviour when the <see cref="IGameEvent{T}"/> is invoked.
        /// </summary>
        /// <param name="value"> Data to pass through. </param>
        void OnInvoked(T value);
    }
}