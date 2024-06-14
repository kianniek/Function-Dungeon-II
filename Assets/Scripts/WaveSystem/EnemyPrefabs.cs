using UnityEngine;
using System;

namespace WaveSystem
{
    [Serializable]
    public class EnemyPrefabs
    {
        [SerializeField] private MonoBehaviour enemyPrefab;
        [SerializeField] private int enemyCount;
        
        public MonoBehaviour EnemyPrefab => enemyPrefab;
        public int EnemyCount => enemyCount;
    }
}