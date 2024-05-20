using System.Collections.Generic;
using Events.GameEventListeners;
using UnityEngine.Events;

namespace Events.GameEvents
{
    /// <inheritdoc cref="IGameEvent{T}"/>
    public class GameEventBase<T> : GameEvent
    {
        private readonly List<IGameEventListener<T>> _typedListeners = new();
        private readonly List<UnityAction<T>> _typedActions = new();

        public void Invoke(T value)
        {
            Invoke();
            
            for (var i = _typedListeners.Count - 1; i >= 0; i--) 
                _typedListeners[i].OnInvoked(value);

            for (var i = _typedActions.Count - 1; i >= 0; i--) 
                _typedActions[i](value);
        }

        public void AddListener(UnityAction<T> callback)
        {
            if (_typedActions.Contains(callback)) return;
            
            _typedActions.Add(callback);
        }

        public void RemoveListener(UnityAction<T> callback)
        {
            if (!_typedActions.Contains(callback)) return;
            
            _typedActions.Remove(callback);
        }

        public override void RemoveAllListeners()
        {
            base.RemoveAllListeners();
            
            _typedListeners.Clear();
            _typedActions.Clear();
        }

        public void AddListener(IGameEventListener<T> eventListener)
        {
            if (_typedListeners.Contains(eventListener)) return;
            
            _typedListeners.Add(eventListener);
        }

        public void RemoveListener(IGameEventListener<T> eventListener)
        {
            if (!_typedListeners.Contains(eventListener)) return;
            
            _typedListeners.Remove(eventListener);
        }
    }
}