using GameEvent.Events;
using UnityEngine;
using UnityEngine.Events;

namespace GameEvent.Listeners
{
    /// <inheritdoc cref="IGameEventListener{T}"/>
    /// <typeparam name="TE"> The <see cref="GameEventBase{T}"/>-type to listen to. </typeparam>
    /// <typeparam name="TU"> The <see cref="UnityEvent{T}"/> to respond with. </typeparam>
    /// <typeparam name="T"></typeparam>
    public class GameEventListenerBase<T, TE, TU> : MonoBehaviour, IGameEventListener<T>
        where TE : GameEventBase<T>
        where TU : UnityEvent<T>
    {
        [SerializeField] private TE listenTo;

        [SerializeField] private TU response;

        public void OnInvoked(T value)
        {
            response.Invoke(value);
        }

        private void OnEnable()
        {
            listenTo.AddListener(this);
        }

        private void OnDisable()
        {
            listenTo.RemoveListener(this);
        }
    }
}