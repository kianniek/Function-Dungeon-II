using Events.GameEvents.Typed;
using UnityEngine;

namespace Towers
{
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

        private void SetPositionToOrigin(GameObject gridObject)
        {
            transform.position = new Vector3(gridObject.transform.position.x, 4, gridObject.transform.position.z);
        }
    }
}
