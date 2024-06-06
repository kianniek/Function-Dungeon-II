using Events.GameEvents.Typed;
using UnityEngine;

namespace FlowerSystem
{
    [CreateAssetMenu(fileName = "FlowerCounter", menuName = "FlowerCounter", order = 1)]
    public class FlowerCounter : ScriptableObject
    {
        [Header("Events")]
        [SerializeField] private IntGameEvent onFlowerChange;

        private int _currentFlowerCount;

        private int CurrentFlowerCount
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

        public int FlowerCount => CurrentFlowerCount;

        /// <summary>
        /// Increase amount of flowers player has
        /// </summary>
        /// <param name="amount">Amount to add</param>
        public void Increase(int amount)
        {
            CurrentFlowerCount += amount;
        }

        /// <summary>
        /// Decrease amount of flowers player has
        /// </summary>
        /// <param name="amount">Amount to substract</param>
        public void Decrease(int amount)
        {
            CurrentFlowerCount -= amount;
        }
    }
}
