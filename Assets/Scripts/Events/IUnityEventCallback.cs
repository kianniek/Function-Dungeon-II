using UnityEngine.Events;

namespace Events
{
    /// <summary>
    /// Exposes unity event based callbacks.
    /// </summary>
    public interface IUnityEventCallback
    {
        /// <summary>
        /// Adds the specified callback listener.
        /// </summary>
        /// <param name="callback"> Listening delegate (method). </param>
        void AddCallback(UnityAction callback);

        /// <summary>
        /// Removes the specified callback listener.
        /// </summary>
        /// <param name="callback"> Listening delegate (method). </param>
        void RemoveCallback(UnityAction callback);

        /// <summary>
        /// Removes all callback listeners.
        /// </summary>
        void RemoveAllCallbacks();
    }
}