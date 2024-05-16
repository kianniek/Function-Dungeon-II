using Events.GameEventListeners;
using UnityEngine;
using UnityEngine.Events;

namespace Events.GameEvents
{
    /// <summary>
    /// Game wide event based on <see cref="ScriptableObject"/>.
    /// </summary>
    /// <remarks> Can be used from the editor in conjunction with <see cref="IGameEventListener"/>. </remarks>
    public interface IGameEvent
    {
        /// <summary>
        /// Fires the event.
        /// </summary>
        void Invoke();
        
        /// <summary>
        /// Adds an <see cref="IGameEventListener"/> to its listeners.
        /// </summary>
        /// <param name="eventListener"> The <see cref="IGameEventListener"/> object to add. </param>
        void AddListener(IGameEventListener eventListener);

        /// <summary>
        /// Removes an <see cref="IGameEventListener"/> from its listeners.
        /// </summary>
        /// <param name="eventListener"> The <see cref="IGameEventListener"/> object to remove. </param>
        void RemoveListener(IGameEventListener eventListener);
        
        /// <summary>
        /// Adds an <see cref="UnityAction"/> to its listeners.
        /// </summary>
        /// <param name="callback"> The <see cref="UnityAction"/> object to add. </param>
        void AddListener(UnityAction callback);

        /// <summary>
        /// Removes an <see cref="UnityAction"/> from its listeners.
        /// </summary>
        /// <param name="callback"> The <see cref="UnityAction"/> object to remove. </param>
        void RemoveListener(UnityAction callback);

        /// <summary>
        /// Removes all listening delegates.
        /// </summary>
        void RemoveAllListeners();
    }
}