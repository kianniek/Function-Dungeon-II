using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using Utils;
using WorldGrid;

namespace WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private bool spawnWavesAuto;
        [SerializeField] private bool spawnWavesOnStart;
        [SerializeField] private EnemyWave[] waves;
        [SerializeField] private UnityEvent onWaveCompleted;
        
        [Header("References To Pass to Spawned Objects")]
        [SerializeField] private GridGenerator gridGenerator;
        
        
        //use object pooling to spawn enemies
        private List<ObjectPool<EnemyBehaviorController>> _enemyPool = new();
        
        private int currentWaveIndex;
        private int enemiesLeftInWave;
        
        public void Awake()
        {
            foreach (var enemyWave in waves)
            {
                foreach (var enemyPrefab in enemyWave.EnemyPrefab)
                {
                    _enemyPool.Add(new ObjectPool<EnemyBehaviorController>(enemyPrefab.EnemyPrefab, enemyPrefab.EnemyCount));
                }
            }
        }
        
        public void Start()
        {
            if (spawnWavesOnStart)
            {
                SpawnWaves();
            }
        }
        
        public void SpawnWaves()
        {
            if (waves.Length > 0)
            {
                StartCoroutine(InitializeWave(waves[currentWaveIndex]));
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator InitializeWave(EnemyWave wave)
        {
            yield return new WaitForSeconds(wave.SpawnInterval);
            
            foreach (var enemyPrefabs in wave.EnemyPrefab)
            {
                enemiesLeftInWave = wave.EnemyCount;
                Debug.Log($"Spawning {wave.EnemyCount} {enemyPrefabs.EnemyPrefab.name}");
                for (var i = 0; i < wave.EnemyCount; i++)
                {
                    enemiesLeftInWave--;
                    var enemy = _enemyPool.Find(pool => pool.GetPooledObject()).GetPooledObject();
                    
                    enemy.transform.position = wave.SpawnLocation;
                    enemy.GridGenerator = gridGenerator;
                    enemy.gameObject.SetActive(true);
                    Debug.Log($"Spawned {enemyPrefabs.EnemyPrefab.name}");
                    
                    yield return new WaitForSeconds(wave.SpawnInterval);
                }
                
                onWaveCompleted.Invoke();
                currentWaveIndex++;
                
                if (currentWaveIndex < waves.Length && spawnWavesAuto)
                {
                    StartCoroutine(InitializeWave(waves[currentWaveIndex]));
                }
                
                yield return null;
            }
        }
        
        public int GetEnemiesLeftInWave()
        {
            return enemiesLeftInWave;
        }
        
        public int GetWavesLeft()
        {
            return waves.Length - currentWaveIndex;
        }
    }
}