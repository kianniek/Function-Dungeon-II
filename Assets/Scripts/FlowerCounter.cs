using System.Collections;
using System.Collections.Generic;
using Events.GameEvents.Typed;
using UnityEngine;

public class FlowerCounter : MonoBehaviour
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

    public void Increase(int amount)
    {
        CurrentFlowerCount += amount;
        onFlowerChange.Invoke();
    }

    public void Decrease(int amount)
    {
        CurrentFlowerCount -= amount;
        onFlowerChange.Invoke();
    }
}
