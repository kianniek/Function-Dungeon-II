using Events.GameEvents.Typed;
using UnityEngine;

namespace Towers
{
    public class TowerUIManager : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private GameObjectGameEvent onShootingTowerPlaced;
        [SerializeField] private GameObjectGameEvent onBombTowerPlaced;

        [Header("UI objects")]
        [SerializeField] private GameObject shootingTowerUI;
        [SerializeField] private GameObject bombTowerUI;
        [SerializeField] private GameObject grid;

        private void Awake()
        {
            onShootingTowerPlaced.AddListener(EnableShootingTowerUI);
            onBombTowerPlaced.AddListener(EnableBombTowerUI);
        }

        private void EnableShootingTowerUI()
        {
            shootingTowerUI.SetActive(true);
            grid.SetActive(true);
        }
        private void EnableBombTowerUI()
        {
            bombTowerUI.SetActive(true);
            grid.SetActive(true);   
        }
    }
}

