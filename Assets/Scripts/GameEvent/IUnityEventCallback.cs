using UnityEngine.Events;

namespace GameEvent
{
    /// <summary>
    /// Exposes unity event based callbacks.
    /// </summary>
    /// <typeparam name="T"> Value type used in the callback. </typeparam>
    public interface IUnityEventCallback<T>
    {
        /// <summary>
        /// Adds the specified one-argument callback listener.
        /// </summary>
        /// <param name="callback"> Listening delegate (method). </param>
        void AddCallback(UnityAction<T> callback);

        /// <summary>
        /// Removes the specified one-argument callback listener.
        /// </summary>
        /// <param name="callback"> Listening delegate (method). </param>
        void RemoveCallback(UnityAction<T> callback);
        
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