using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;
using Utils;
using WorldGrid;

namespace WaveSystem
{
    public class WaveManager : MonoBehaviour
    {
        private readonly List<SimpleObjectPool<NavMeshAgent>> _enemyPool = new();
        
        [Header("Spawn Settings")]
        [SerializeField] private bool spawnWavesAuto;
        [SerializeField] private bool spawnWavesOnStart;
        [SerializeField] private List<EnemyWave> waves;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onWaveCompleted;
        
        [Header("World Reference")]
        [SerializeField] private GridGenerator gridGenerator;
        
        private int _currentWaveIndex;

        public int CurrentWave => _currentWaveIndex + 1;

        public int EnemiesLeftInWave { get; private set; }

        public int WavesLeft => waves.Count - CurrentWave;

        private void Awake()
        {
            foreach (var enemyPrefab in waves.SelectMany(enemyWave => enemyWave.EnemyPrefab)) 
                _enemyPool.Add(new SimpleObjectPool<NavMeshAgent>(enemyPrefab.EnemyPrefab, enemyPrefab.EnemyCount));
        }
        
        private void Start()
        {
            if (spawnWavesOnStart) 
                SpawnWaves();
        }
        
        public void SpawnWaves()
        {
            if (waves.Any()) 
                StartCoroutine(InitializeWave(waves[_currentWaveIndex]));
        }
        
        private IEnumerator InitializeWave(EnemyWave wave)
        {
            yield return new WaitForSeconds(wave.SpawnInterval);

            for (var index = 0; index < wave.EnemyPrefab.Length; index++)
            {
                EnemiesLeftInWave = wave.EnemyCount;

                for (var i = 0; i < wave.EnemyCount; i++)
                {
                    EnemiesLeftInWave--;
                    
                    var enemy = _enemyPool.Find(pool => pool.GetPooledObject()).GetPooledObject();

                    enemy.transform.position = gridGenerator.PathStartPosition;
                    enemy.gameObject.SetActive(true);
                    enemy.enabled = true;
                    enemy.SetDestination(gridGenerator.PathEndPosition);

                    yield return new WaitForSeconds(wave.SpawnInterval);
                }

                onWaveCompleted.Invoke();
                
                _currentWaveIndex++;

                if (_currentWaveIndex < waves.Count && spawnWavesAuto) 
                    StartCoroutine(InitializeWave(waves[_currentWaveIndex]));

                yield return null;
            }
        }
    }
}