using Events.GameEventListeners;
using UnityEngine;
using UnityEngine.Events;

namespace Events.GameEvents
{
    /// <summary>
    /// Game wide event based on <see cref="ScriptableObject"/>.
    /// </summary>
    /// <remarks> Can be used from the editor in conjunction with  <see cref="IGameEventListener{T}"/>. </remarks>
    /// <typeparam name="T"> Defines which value-type is to be passed through. </typeparam>
    public interface IGameEvent<T> : IGameEvent
    {
        /// <summary>
        /// Fires the event.
        /// </summary>
        /// <param name="value"> Value to pass through. </param>
        void Invoke(T value);
        
        /// <summary>
        /// Adds an <see cref="IGameEventListener{T}"/> to its listeners.
        /// </summary>
        /// <param name="eventListener"> The <see cref="IGameEventListener{T}"/> object to add. </param>
        void AddListener(IGameEventListener<T> eventListener);

        /// <summary>
        /// Removes an <see cref="IGameEventListener{T}"/> from its listeners.
        /// </summary>
        /// <param name="eventListener"> The <see cref="IGameEventListener{T}"/> object to remove. </param>
        void RemoveListener(IGameEventListener<T> eventListener);
        
        /// <summary>
        /// Adds an <see cref="UnityAction{T}"/> to its listeners.
        /// </summary>
        /// <param name="callback"> The <see cref="UnityAction{T}"/> object to add. </param>
        void AddListener(UnityAction<T> callback);

        /// <summary>
        /// Removes an <see cref="UnityAction{T}"/> from its listeners.
        /// </summary>
        /// <param name="callback"> The <see cref="UnityAction{T}"/> object to remove. </param>
        void RemoveListener(UnityAction<T> callback);
    }
}