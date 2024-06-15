using System.Linq;
using UnityEngine;

namespace WaveSystem
{
    [CreateAssetMenu(fileName = "New Enemy Wave", menuName = "EnemyWave")]
    public class EnemyWave : ScriptableObject
    {
        [SerializeField] private EnemyPrefabs[] enemyPrefabs;
        [SerializeField] private float spawnInterval;
        [SerializeField] private Vector3 spawnLocation;
        
        public EnemyPrefabs[] EnemyPrefab => enemyPrefabs;
        
        public int EnemyCount => enemyPrefabs.Sum(enemy => enemy.EnemyCount);

        public float SpawnInterval => spawnInterval;
        
        public Vector3 SpawnLocation => spawnLocation;
    }
}