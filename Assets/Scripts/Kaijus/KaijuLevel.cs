using System.Linq;
using UnityEngine;

namespace Kaijus
{
    [CreateAssetMenu(fileName = "New Kaiju Level", menuName = "KaijuLevel")]
    public class KaijuLevel : ScriptableObject
    {
        [SerializeField] private GameObject[] kaijuPrefabs;

        public GameObject[] KaijuPrefabs => kaijuPrefabs;

        public int KaijuCount => kaijuPrefabs.Count();
        public int CurrentKaijuInLevel;

    }
}
