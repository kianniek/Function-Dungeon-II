using Events.GameEvents.Typed;
using UnityEngine;

public class GridPositionChanger : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private GameObjectGameEvent onShootingTowerPlaced;
    [SerializeField] private GameObjectGameEvent onBombTowerPlaced;

    private void Awake()
    {
        onShootingTowerPlaced.AddListener(SetPositionToOrigin);
        onBombTowerPlaced.AddListener(SetPositionToOrigin);
    }

    private void SetPositionToOrigin(GameObject gameObject)
    {
        transform.position = new Vector3(gameObject.transform.position.x, 4, gameObject.transform.position.z);
    }
}
