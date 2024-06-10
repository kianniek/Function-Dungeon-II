using UnityEngine;

namespace WorldGrid
{
    /// <summary>
    /// Used to quickly generate a grid 
    /// </summary>
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GridTile gridTile;
        [SerializeField] private PlaceableTile placeableGridTile;
        [SerializeField] private PathTile pathGridTile;
        [SerializeField] private GridData gridData;

        private Transform _gridTileTransform;

        private void Awake()
        {
            _gridTileTransform = gridTile.transform;
        }

        /// <summary>
        /// Instantiates the right gridtiles at the postions defined in PathData
        /// </summary>
        private void Start()
        {
            for (var i = 0; i < gridData.XGridSize; i++)
            {
                for (var j = 0; j < gridData.YGridSize; j++)
                {
                    switch (gridData.generatedGrid[i, j])
                    {
                        case (GridTileTypes.Empty):
                            Instantiate(gridTile, new Vector3(i * _gridTileTransform.localScale.x, j * _gridTileTransform.localScale.y, 0), Quaternion.identity, transform);
                            break;
                        case (GridTileTypes.Placeable):
                            Instantiate(placeableGridTile, new Vector3(i * _gridTileTransform.localScale.x, j * _gridTileTransform.localScale.y, 0), Quaternion.identity, transform);
                            break;
                        case (GridTileTypes.Path):
                            Instantiate(pathGridTile, new Vector3(i * _gridTileTransform.localScale.x, j * _gridTileTransform.localScale.y, 0), Quaternion.identity, transform);
                            break;
                    }
                }
            }
        }
    }
}

