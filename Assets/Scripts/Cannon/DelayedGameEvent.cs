using System.Collections;
using Events.GameEvents;
using UnityEngine;

namespace Cannon
{
    public class DelayedGameEvent : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private float delay = 1f;
        
        /// <summary>
        /// Invokes the game event after the delay
        /// </summary>
        public void InvokeDelayed()
        {
            StartCoroutine(DelayedInvoke());
        }
        
        private IEnumerator DelayedInvoke()
        {
            yield return new WaitForSeconds(delay);
            
            gameEvent.Invoke();
        }
    }
}