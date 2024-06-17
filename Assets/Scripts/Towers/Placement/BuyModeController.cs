using FlowerSystem;
using Towers.Configuration;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Towers.Placement
{
    /// <summary>
    /// Controls the buy mode of the game.
    /// </summary>
    [RequireComponent(typeof(SuitablePlacementFinder))]
    public class BuyModeController : MonoBehaviour
    {
        [Header("Currency")]
        [SerializeField] private FlowerCounter flowerCounter;
        
        [Header("Buy Buttons")]
        [SerializeField] private Button shootingTowerButton;
        [SerializeField] private TowerVariables shootingTowerVariables;
        [SerializeField] private Button bombTowerButton;
        [SerializeField] private TowerVariables bombTowerVariables;
        
        [Header("World")]
        [SerializeField] private Transform world;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onTowerInstantiated = new();
        
        private SuitablePlacementFinder _suitablePlacementFinder;
        private TowerConfigurator _selectedTower;

        /// <summary>
        /// The selected tower to place.
        /// </summary>
        public TowerConfigurator SelectedTower
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

        /// <summary>
        /// Recalculates the flowers after a tower has been placed.
        /// </summary>
        public void RecalculateFlowers()
        {
            if (SelectedTower.TowerVariables == shootingTowerVariables)
            {
                flowerCounter.CurrentFlowerCount -= shootingTowerVariables.FlowerCost;
            }
            else if (SelectedTower.TowerVariables == bombTowerVariables)
            {
                flowerCounter.CurrentFlowerCount -= bombTowerVariables.FlowerCost;
            }
        }
        
        /// <summary>
        /// Enables the buy buttons.
        /// </summary>
        public void EnableBuyButtons()
        {
            shootingTowerButton.interactable = flowerCounter.CurrentFlowerCount >= shootingTowerVariables.FlowerCost;
            bombTowerButton.interactable = flowerCounter.CurrentFlowerCount >= bombTowerVariables.FlowerCost;
        }

        private void PlaceTower(Vector3 position)
        {
            if (!SelectedTower)
                return;
            
            position.y += 1f;

            Instantiate(SelectedTower, position, Quaternion.identity, world);
            
            RecalculateFlowers();
            
            SelectedTower = null;
            
            onTowerInstantiated.Invoke();
            
            shootingTowerButton.interactable = false;
            bombTowerButton.interactable = false;
        }
    }
}