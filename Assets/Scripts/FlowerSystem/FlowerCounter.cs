using Events.GameEvents.Typed;
using UnityEngine;

namespace FlowerSystem
{
    /// <summary>
    /// Scriptable object which holds the amount of flowers the player has.
    /// </summary>
    [CreateAssetMenu(fileName = "FlowerCounter", menuName = "FlowerCounter", order = 1)]
    public class FlowerCounter : ScriptableObject
    {
        [SerializeField] private IntGameEvent onFlowerChange;

        private int _currentFlowerCount;

        public int CurrentFlowerCount
        {
            get => _currentFlowerCount;
            set
            {
                if (value < 0)
                    return;

                _currentFlowerCount = value;

                onFlowerChange?.Invoke(value);
            }
        }
    }
}
