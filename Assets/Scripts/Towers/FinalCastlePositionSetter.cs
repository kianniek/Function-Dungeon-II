using Health;
using UnityEngine;
using WorldGrid;

namespace Towers
{
    public class FinalCastlePositionSetter : MonoBehaviour
    {
        private const int FinalCastleYPosition = 2;
        
        [Header("World Settings")]
        [SerializeField] private GridGenerator gridGenerator;
        [SerializeField] private Transform world;
        
        [Header("Prefab")]
        [SerializeField] private Damageable finalCastle;
        
        private void Start()
        {
            var position = gridGenerator.PathEndPosition;
            
            position.y = FinalCastleYPosition;
            
            Instantiate(finalCastle, position, Quaternion.identity, world);
        }
    }
}
