using Events.GameEvents.Typed;
using UnityEngine;

public class ShootTower : MonoBehaviour
{
    [SerializeField] private GameObjectGameEvent onShootingTowerPlaced;

    private void Start()
    {
        onShootingTowerPlaced.Invoke();
    }
}
