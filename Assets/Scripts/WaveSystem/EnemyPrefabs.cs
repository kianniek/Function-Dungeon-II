using UnityEngine;
using System;
using Enemies;

namespace WaveSystem
{
    [Serializable]
    public class EnemyPrefabs
    {
        [SerializeField] private EnemyBehaviorController enemyPrefab;
        [SerializeField] private int enemyCount;
        
        public EnemyBehaviorController EnemyPrefab => enemyPrefab;
        public int EnemyCount => enemyCount;
    }
}