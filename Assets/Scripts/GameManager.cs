using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    internal int _enemyKillCount, _enemyCount;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        UpdateCount();
    }

    private void UpdateCount()
    {
        _enemyKillCount = 0;
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            _enemyCount++;
        }
    }
}
