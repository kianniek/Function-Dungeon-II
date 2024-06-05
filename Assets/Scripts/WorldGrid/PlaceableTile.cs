using UnityEngine;
using UnityEngine.Events;

namespace WorldGrid
{
    public class PlaceableTile : MonoBehaviour
    {
        [SerializeField] private UnityEvent onTowerPlacedEvent = new();
        private bool _hasTower;

        public void OnTowerPlaced()
        {
            onTowerPlacedEvent.Invoke();
            _hasTower = true;
        }

        public void SubscribeToTowerPlacedEvent(UnityAction action)
        {
            onTowerPlacedEvent.AddListener(action);
        }
    }
}