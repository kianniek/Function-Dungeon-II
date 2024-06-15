using UnityEngine;
using System;
using ObjectMovement;

namespace WaveSystem
{
    [Serializable]
    public class EnemyPrefabs
    {
        [SerializeField] private BloomShroomMovement enemyPrefab;
        [SerializeField] private int enemyCount;
        
        public BloomShroomMovement EnemyPrefab => enemyPrefab;
        
        public int EnemyCount => enemyCount;
    }
}