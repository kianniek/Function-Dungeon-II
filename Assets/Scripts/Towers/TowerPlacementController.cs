using Health;
using UnityEngine;

namespace Towers
{
    [RequireComponent(typeof(SuitablePlacementFinder))]
    public class TowerPlacementController : MonoBehaviour
    {
        private SuitablePlacementFinder _suitablePlacementFinder;
        private CollectiveHealth _selectedTower;

        public CollectiveHealth SelectedTower
        {
            private get => _selectedTower;
            set
            {
                _selectedTower = value;
                _suitablePlacementFinder.enabled = _selectedTower;
            }
        }

        private void Awake()
        {
            _suitablePlacementFinder = GetComponent<SuitablePlacementFinder>();
        }

        private void OnEnable()
        {
            _suitablePlacementFinder.SubscribeToOnSuitablePlacement(PlaceTower);
        }

        private void OnDisable()
        {
            _suitablePlacementFinder.UnsubscribeFromOnSuitablePlacement(PlaceTower);
        }

        private void PlaceTower(Vector3 position)
        {
            if (!SelectedTower)
                return;

            Instantiate(SelectedTower, position, Quaternion.identity);
            
            SelectedTower = null;
        }
    }
}