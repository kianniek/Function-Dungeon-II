using UnityEngine;

namespace WorldGrid
{
    public class PlaceableTile : MonoBehaviour
    {
        private bool _hasTower;

        public void OnTowerPlaced()
        {
            _hasTower = true;
        }
    }
}