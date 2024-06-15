using UnityEngine;
using System;
using ObjectMovement;
using UnityEngine.AI;

namespace WaveSystem
{
    [Serializable]
    public class EnemyPrefabs
    {
        [SerializeField] private NavMeshAgent enemyPrefab;
        [SerializeField] private int enemyCount;
        
        public NavMeshAgent EnemyPrefab => enemyPrefab;
        
        public int EnemyCount => enemyCount;
    }
}