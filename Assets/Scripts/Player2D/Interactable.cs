using UnityEngine;
using UnityEngine.Events;

namespace Player2D
{
    [RequireComponent(typeof(Collider2D))]
    public class Interactable : MonoBehaviour
    {
        // Event invoked when the player interacts with this object.
        [SerializeField] private UnityEvent onInteraction = new();

        // Event invoked when the player enters the range.
        [SerializeField] private UnityEvent onPlayerEnterRange = new();

        // Event invoked when the player exits the range.
        [SerializeField] private UnityEvent onPlayerExitRange = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var playerController = collision.GetComponent<PlayerController2D>();

            if (playerController)
            {
                playerController.SetInteractable(this);
                onPlayerEnterRange.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var playerController = collision.GetComponent<PlayerController2D>();

            if (!playerController) 
                return;
            
            playerController.SetInteractable(null);
            onPlayerExitRange.Invoke();
        }

        /// <summary>
        /// A method used to invoke the onInteraction unity event
        /// </summary>
        public void InvokeInteraction()
        {
            onInteraction.Invoke();
        }
    }
}