using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Delay
{
    public class DelayedResponse : MonoBehaviour
    {
        [Tooltip("The delay in seconds before the response is triggered")]
        [SerializeField] private float delay = 1f;

        [Header("Event")] 
        [SerializeField] private UnityEvent response = new();

        public void Invoke()
        {
            StartCoroutine(Response());
        }
        
        private IEnumerator Response()
        {
            yield return new WaitForSeconds(delay);
            
            response.Invoke();
        }
    }
}