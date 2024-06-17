using System;
using System.Collections.Generic;
using Events.GameEvents;
using UnityEngine;

public class KaijuHealth : MonoBehaviour
{
    [SerializeField] private List<GameObject> weakpoints = new();
    [SerializeField] private GameEvent onHitpointHit;

    private int _health;

    private void Awake()
    {
        onHitpointHit.AddListener(SubtractHealth);
    }

    private void Start()
    {
        foreach (var weakpoint in weakpoints)
        {
            _health++;
        }
    }

    private void SubtractHealth()
    {
        _health--;
        DieCheck();
    }

    private void DieCheck()
    {
        if (_health == 0)
        {
            //TODO handle next wave etc
            Debug.Log("Dead");
        }
    }
}
