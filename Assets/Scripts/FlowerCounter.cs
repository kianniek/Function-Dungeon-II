using Events.GameEvents.Typed;
using UnityEngine;

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

    public void Increase(int amount)
    {
        CurrentFlowerCount += amount;
    }

    public void Decrease(int amount)
    {
        CurrentFlowerCount -= amount;
    }
}
