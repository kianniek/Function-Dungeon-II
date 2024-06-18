using System.Linq;
using UnityEngine;

namespace Kaijus
{
    [CreateAssetMenu(fileName = "New Kaiju Wave", menuName = "KaijuWave")]
    public class KaijuWave : MonoBehaviour
    {
        [SerializeField] private GameObject[] kaijuPrefabs;

        public GameObject[] KaijuPrefabs => kaijuPrefabs;

        public int KaijuCount => kaijuPrefabs.Count();

    }
}
