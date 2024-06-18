using Events.GameEvents;
using UnityEngine;

namespace Kaijus
{
    public class KaijuWaveManager : MonoBehaviour
    {
        [Header("Variables for pre-defined wave")]
        [Tooltip("Insert a pre-defined KaijuWave scriptable object in here")]
        [SerializeField] private KaijuWave waveToPlay;

        /// <summary>
        /// Random wave is conditinial bool for this block of variables
        /// </summary>
        [Header("Variables for random wave")]
        [SerializeField] private bool randomWave;
        [Tooltip("Prefabs for kaijus which can be included in the random wave")]
        [SerializeField] private GameObject[] kaijuPrefabs;
        [Tooltip("Amount of kaijus in the random wave")]
        [SerializeField] private int kaijusInWave = 3;

        [Header("Events")]
        [SerializeField] private GameEvent onKaijuDie;

        private GameObject[] _kaijuWave;
        private int _currentKaijuInWave;

        private void Awake()
        {
            onKaijuDie.AddListener(NextKaijuInWave);
        }

        private void Start()
        {
            if (randomWave)
            {
                GenerateRandomWave();
            }
            else
            {
                GeneratePredefinedWave();
            }
        }

        /// <summary>
        /// Generates a random kaijuwave and saves it in an array. Spawns the first kaiju
        /// </summary>
        private void GenerateRandomWave()
        {
            _kaijuWave = new GameObject[kaijusInWave];
            for (var i = 0; i < kaijusInWave; i++)
            {
                _kaijuWave[i] = kaijuPrefabs[Random.Range(0, kaijuPrefabs.Length)];
            }
            SpawnKaiju();
        }

        /// <summary>
        /// Generates a pre-defined kaijuwave and saves it in an array. Spawns the first kaiju
        /// </summary>
        private void GeneratePredefinedWave()
        {
            _kaijuWave = new GameObject[waveToPlay.KaijuCount];
            for (var i = 0; i < waveToPlay.KaijuCount; i++)
            {
                _kaijuWave[i] = waveToPlay.KaijuPrefabs[i];
            }
            kaijusInWave = waveToPlay.KaijuCount;
            SpawnKaiju();
        }

        /// <summary>
        /// Checks if there are any kaijus left in the wave. If yes spawn the next kaiju
        /// </summary>
        private void NextKaijuInWave()
        {
            if (_currentKaijuInWave == kaijusInWave)
            {
                //TODO player has killed all kaijus in a wave
            }
            _currentKaijuInWave++;
            SpawnKaiju();
        }

        /// <summary>
        /// Spawns the kaiju with index _currentKaijuInWave.
        /// </summary>
        private void SpawnKaiju()
        {
            Instantiate(_kaijuWave[_currentKaijuInWave]);
        }
    }
}
