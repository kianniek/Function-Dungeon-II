using UnityEngine;
using UnityEngine.Events;

namespace WorldGrid
{
    public class PlaceableTile : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTowerPlacedEvent = new();
        private bool _hasTower;

        /// <summary>
        /// Handles what happens when an tower gets place (still WIP, waiting for tower implementation)
        /// </summary>
        public void OnTowerPlaced()
        {
            if (!_hasTower)
            {
                onTowerPlacedEvent.Invoke();
                _hasTower = true;
            }
        }

        /// <summary>
        /// Adds a listener to the onTowerPlacedEvent
        /// </summary>
        /// <param name="action">The function to add</param>
        public void SubscribeToTowerPlacedEvent(UnityAction action)
        {
            onTowerPlacedEvent.AddListener(action);
        }
    }
}