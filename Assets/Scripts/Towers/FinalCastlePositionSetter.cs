using UnityEngine;
using WorldGrid;

namespace Towers
{
    public class FinalCastlePositionSetter : MonoBehaviour
    {
        [SerializeField] private GridGenerator gridGenerator;
        [SerializeField] private Transform finalCastle;

        private int _finalCastleYPosition = 2;

        private void Start()
        {
            finalCastle.position = new Vector3(gridGenerator.PathEndPosition.x, _finalCastleYPosition, gridGenerator.PathEndPosition.z);
        }
    }
}
