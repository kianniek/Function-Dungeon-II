using Events.GameEvents.Typed;
using UnityEngine;

namespace Towers
{
    public class BombTower : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private GameObjectGameEvent onBombTowerPlaced;
        [SerializeField] private Vector2GameEvent bombCoordinatesSet;

        [Header("Variables")]
        [SerializeField] private TowerVariables bombTowerVariables;

        [Header("Sprites")]
        [SerializeField] private GameObject rangeCircle;

        private Vector2 _bombPosition;

        private void Start()
        {
            onBombTowerPlaced.Invoke(gameObject);
            bombCoordinatesSet.AddListener(BombCoordinatesSet);
        }

        /// <summary>
        /// Sets bombcoordinate from event
        /// </summary>
        /// <param name="bombPosition"></param>
        private void BombCoordinatesSet(Vector2 bombPosition)
        {
            _bombPosition = bombPosition;
            rangeCircle.SetActive(false);
            bombCoordinatesSet.RemoveAllListeners();
        }
    }
}
