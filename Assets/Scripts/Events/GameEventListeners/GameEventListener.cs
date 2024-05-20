using Events.GameEvents;
using UnityEngine;
using UnityEngine.Events;

namespace Events.GameEventListeners
{
    /// <inheritdoc cref="IGameEventListener"/>
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        [Header("Game Event")]
        [SerializeField] private GameEvent listenTo;
        
        [Header("Callbacks")]
        [SerializeField] private UnityEvent response;

        public void OnInvoked()
        {
            response.Invoke();
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