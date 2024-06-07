using Events.GameEvents.Typed;
using UnityEngine;

public class BombTower : MonoBehaviour
{
    [SerializeField] private GameObjectGameEvent onBombTowerPlaced;
    [SerializeField] private Vector2 bombPosition;

    private void Start()
    {
        onBombTowerPlaced.Invoke();
    }
}
