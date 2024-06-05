using UnityEngine;

namespace WorldGrid
{
    public class PlaceableTile : MonoBehaviour
    {
        //TODO event for tower has placed
        private bool _hasTower;

        public void OnTowerPlaced()
        {
            _hasTower = true;
        }
    }
}