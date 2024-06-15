using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ObjectMovement;
using Utils;
using WorldGrid;

namespace WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        private readonly List<ObjectPool<BloomShroomMovement>> _enemyPool = new();
        
        [Header("Spawn Settings")]
        [SerializeField] private bool spawnWavesAuto;
        [SerializeField] private bool spawnWavesOnStart;
        [SerializeField] private List<EnemyWave> waves;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onWaveCompleted;
        
        [Header("World Reference")]
        [SerializeField] private GridGenerator gridGenerator;
        
        private int _currentWaveIndex;
        private int _enemiesLeftInWave;
        
        public void Awake()
        {
            foreach (var enemyPrefab in waves.SelectMany(enemyWave => enemyWave.EnemyPrefab))
            {
                _enemyPool.Add(new ObjectPool<BloomShroomMovement>(enemyPrefab.EnemyPrefab, enemyPrefab.EnemyCount));
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
            if (waves.Any()) 
                StartCoroutine(InitializeWave(waves[_currentWaveIndex]));
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator InitializeWave(EnemyWave wave)
        {
            yield return new WaitForSeconds(wave.SpawnInterval);

            for (var index = 0; index < wave.EnemyPrefab.Length; index++)
            {
                _enemiesLeftInWave = wave.EnemyCount;

                for (var i = 0; i < wave.EnemyCount; i++)
                {
                    _enemiesLeftInWave--;
                    
                    var enemy = _enemyPool.Find(pool => pool.GetPooledObject()).GetPooledObject();

                    enemy.transform.position = gridGenerator.PathStartPosition;
                    enemy.gameObject.SetActive(true);
                    enemy.SetAndStartPath(gridGenerator.PathEndPosition);

                    yield return new WaitForSeconds(wave.SpawnInterval);
                }

                onWaveCompleted.Invoke();
                _currentWaveIndex++;

                if (_currentWaveIndex < waves.Count && spawnWavesAuto)
                {
                    StartCoroutine(InitializeWave(waves[_currentWaveIndex]));
                }

                yield return null;
            }
        }
        
        public int GetEnemiesLeftInWave()
        {
            return _enemiesLeftInWave;
        }
        
        public int GetWavesLeft()
        {
            return waves.Count - _currentWaveIndex;
        }
    }
}