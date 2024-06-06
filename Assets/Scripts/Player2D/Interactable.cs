using UnityEngine;
using UnityEngine.Events;

namespace Player2D
{
    [RequireComponent(typeof(Collider2D))]
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private UnityEvent onInteraction = new(); // Event invoked when the player interacts with this object.

        /// <summary>
        /// A method used to invoke the onInteraction unity event
        /// </summary>
        public void InvokeInteraction()
        {
            onInteraction.Invoke();
        }
    }
}