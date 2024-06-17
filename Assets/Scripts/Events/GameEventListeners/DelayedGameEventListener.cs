using System.Collections;
using Events.GameEvents;
using UnityEngine;
using UnityEngine.Events;

namespace Events.GameEventListeners
{
    public class DelayedGameEventListener : MonoBehaviour, IGameEventListener
    {
        [Header("Delay")]
        [Tooltip("Delay in seconds before invoking the response.")]
        [SerializeField, Min(0.001f)] private float delay = 1f;
        
        [Header("Game Event")]
        [SerializeField] private GameEvent listenTo;
        
        [Header("Callbacks")]
        [SerializeField] private UnityEvent response;
        
        public void OnInvoked()
        {
            StartCoroutine(DelayedResponse());
        }

        private void OnEnable()
        {
            listenTo.AddListener(this);
        }

        private void OnDisable()
        {
            listenTo.RemoveListener(this);
        }
        
        private IEnumerator DelayedResponse()
        {
            yield return new WaitForSeconds(delay);
            
            response.Invoke();
        }
    }
}