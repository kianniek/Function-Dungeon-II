using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Utils;

namespace WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private bool spawnWavesAuto;
        [SerializeField] private EnemyWave[] waves;
        [SerializeField] private UnityEvent onWaveCompleted;
        
        //use object pooling to spawn enemies
        private List<ObjectPool<MonoBehaviour>> _enemyPool = new();
        
        private int currentWaveIndex;
        private int enemiesLeftInWave;
        
        public void Awake()
        {
            foreach (var enemyWave in waves)
            {
                foreach (var enemyPrefab in enemyWave.EnemyPrefab)
                {
                    _enemyPool.Add(new ObjectPool<MonoBehaviour>(enemyPrefab.EnemyPrefab, enemyPrefab.EnemyCount));
                }
            }
        }
        
        public void SpawnWaves()
        {
            if (waves.Length > 0)
            {
                StartCoroutine(InitializeWave(waves[currentWaveIndex]));
            }
        }
        
        private IEnumerator InitializeWave(EnemyWave wave)
        {
            foreach (var enemyPrefabs in wave.EnemyPrefab)
            {
                enemiesLeftInWave = wave.EnemyCount;
                
                for (var i = 0; i < wave.EnemyCount; i++)
                {
                    enemiesLeftInWave--;
                    var enemy = _enemyPool.Find(pool => pool.GetPooledObject()).GetPooledObject();
                    
                    enemy.transform.position = wave.SpawnLocation;
                    enemy.gameObject.SetActive(true);
                    
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