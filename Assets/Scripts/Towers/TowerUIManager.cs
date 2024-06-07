using Events.GameEvents.Typed;
using UnityEngine;

public class TowerUIManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameObjectGameEvent onShootingTowerPlaced;
    [SerializeField] private GameObjectGameEvent onBombTowerPlaced;

    [Header("Gameobjects")]
    [SerializeField] private GameObject shootingTowerUI;
    [SerializeField] private GameObject bombTowerUI;

    private void Awake()
    {
        onShootingTowerPlaced.AddListener(EnableShootingTowerUI);
        onBombTowerPlaced.AddListener(EnableBombTowerUI);
    }

    private void EnableShootingTowerUI()
    {
        shootingTowerUI.SetActive(true);
    }
    private void EnableBombTowerUI()
    {
        bombTowerUI.SetActive(true);
    }
}
