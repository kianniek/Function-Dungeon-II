using Events.GameEvents;
using UnityEngine;

namespace Kaijus
{
    public class KaijuWaveManager : MonoBehaviour
    {
        [SerializeField] private KaijuWave waveToPlay;

        /// <summary>
        /// Random wave is conditinial bool for this block of variables
        /// </summary>
        [SerializeField] private bool randomWave;
        [SerializeField] private GameObject[] kaijuPrefabs;
        [SerializeField] private int kaijusInWave = 3;

        [SerializeField] private GameEvent onKaijuDie;

        private GameObject[] kaijuWave;
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

        private void GenerateRandomWave()
        {
            kaijuWave = new GameObject[kaijusInWave];
            for (var i = 0; i < kaijusInWave; i++)
            {
                kaijuWave[i] = kaijuPrefabs[Random.Range(0, kaijuPrefabs.Length)];
            }
            SpawnKaiju();
        }

        private void GeneratePredefinedWave()
        {
            kaijuWave = new GameObject[waveToPlay.KaijuCount];
            for (var i = 0; i < waveToPlay.KaijuCount; i++)
            {
                kaijuWave[i] = waveToPlay.KaijuPrefabs[i];
            }
            kaijusInWave = waveToPlay.KaijuCount;
            SpawnKaiju();
        }
        private void NextKaijuInWave()
        {
            _currentKaijuInWave++;
            SpawnKaiju();
        }

        private void SpawnKaiju()
        {
            Instantiate(kaijuWave[_currentKaijuInWave]);
        }
    }
}
