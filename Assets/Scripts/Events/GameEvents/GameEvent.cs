using System.Collections.Generic;
using Events.GameEventListeners;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable CS0169 // Field is never used

namespace Events.GameEvents
{
    /// <inheritdoc cref="IGameEvent"/>
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Game Events/Event", order = 0)]
    public class GameEvent : ScriptableObject, IGameEvent
    {
        [Multiline] [SerializeField] private string description;
        
        private readonly List<IGameEventListener> _listeners = new();
        private readonly List<UnityAction> _actions = new();

        public void Invoke()
        {
            for (var i = _listeners.Count - 1; i >= 0; i--) 
                _listeners[i].OnInvoked();
            
            for (var i = _actions.Count - 1; i >= 0; i--) 
                _actions[i]();
        }
        
        public void AddListener(UnityAction callback)
        {
            if (_actions.Contains(callback)) 
                return;
            
            _actions.Add(callback);
        }
        
        public void RemoveListener(UnityAction callback)
        {
            if (!_actions.Contains(callback)) 
                return;
            
            _actions.Remove(callback);
        }
        
        public void AddListener(IGameEventListener eventListener)
        {
            if (_listeners.Contains(eventListener)) 
                return;
            
            _listeners.Add(eventListener);
        }
        
        public void RemoveListener(IGameEventListener eventListener)
        {
            if (!_listeners.Contains(eventListener)) 
                return;
            
            _listeners.Remove(eventListener);
        }
        
        public virtual void RemoveAllListeners()
        {
            _listeners.Clear();
            _actions.Clear();
        }
    }
}