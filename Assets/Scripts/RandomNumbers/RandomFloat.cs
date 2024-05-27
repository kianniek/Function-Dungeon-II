using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Events.GameEvents.Typed;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class RandomFloat : MonoBehaviour
{
    [SerializeField] private bool executeAtStart;
    [SerializeField] private int amountBehindComma;

    [SerializeField] private float minValue;
    [SerializeField] private float maxValue;
    [SerializeField] private FloatEvent onValueChanged;

    // Start is called before the first frame update
    public void Start()
    {
        if (executeAtStart)
            GenerateRandomFloat();
    }

    // Update is called once per frame
    public void GenerateRandomFloat()
    {
        // Generate a random float value between the min and max values
        var randomValue = UnityEngine.Random.Range(minValue, maxValue);

        randomValue = MathF.Round(randomValue, amountBehindComma);

        // Invoke the event with the generated random value
        onValueChanged?.Invoke(randomValue);
    }
}
