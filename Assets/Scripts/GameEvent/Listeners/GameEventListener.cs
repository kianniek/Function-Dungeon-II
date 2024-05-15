using UnityEngine;
using UnityEngine.Events;

namespace GameEvent.Listeners
{
    /// <inheritdoc cref="IGameEventListener"/>
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        [SerializeField] private Events.GameEvent listenTo;

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