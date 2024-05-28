using System.Collections;
using Events.GameEvents;
using UnityEngine;

namespace Delay
{
    /// <summary>
    /// Invokes a game event after a delay.
    /// </summary>
    public class DelayedLevelFailed : MonoBehaviour
    {
        [SerializeField] private GameEvent onLevelFailed;
        [SerializeField] private GameEvent onLevelCleared;
        [SerializeField] private float delay = 1f;
        
        private bool _levelCleared;
        
        private void OnEnable()
        {
            onLevelCleared?.AddListener(SetLevelCleared);
        }
        
        private void OnDisable()
        {
            onLevelCleared?.RemoveListener(SetLevelCleared);
        }
        
        /// <summary>
        /// Invokes the game event after the delay
        /// </summary>
        public void InvokeDelayed()
        {
            StartCoroutine(DelayedInvoke());
        }
        
        private void SetLevelCleared()
        {
            _levelCleared = true;
        }
        
        private IEnumerator DelayedInvoke()
        {
            yield return new WaitForSeconds(delay);
            
            if (_levelCleared)
                yield break;
            
            onLevelFailed.Invoke();
        }
    }
}