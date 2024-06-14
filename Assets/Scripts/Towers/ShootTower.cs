using Events.GameEvents.Typed;
using UnityEngine;

namespace Towers
{
    public class ShootTower : MonoBehaviour
    {
        [SerializeField] private GameObjectGameEvent onShootingTowerPlaced;

        private void Start()
        {
            onShootingTowerPlaced.Invoke();
        }
    }
}
