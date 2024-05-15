using GameEvent.Listeners;
using UnityEngine;
using UnityEngine.Events;

namespace GameEvent.Events
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
        /// Adds a <see cref="IGameEventListener{T}"/> to its listeners.
        /// </summary>
        /// <param name="eventListener"> The <see cref="IGameEventListener{T}"/> object to add. </param>
        void AddListener(IGameEventListener<T> eventListener);

        /// <summary>
        /// Removes a <see cref="IGameEventListener{T}"/> from its listeners.
        /// </summary>
        /// <param name="eventListener"> The <see cref="IGameEventListener{T}"/> object to remove. </param>
        void RemoveListener(IGameEventListener<T> eventListener);
        
        /// <summary>
        /// Adds a <see cref="UnityAction{T}"/> to its listeners.
        /// </summary>
        /// <param name="callback"> The <see cref="UnityAction{T}"/> object to add. </param>
        void AddListener(UnityAction<T> callback);

        /// <summary>
        /// Removes a <see cref="UnityAction{T}"/> from its listeners.
        /// </summary>
        /// <param name="callback"> The <see cref="UnityAction{T}"/> object to remove. </param>
        void RemoveListener(UnityAction<T> callback);
    }
}