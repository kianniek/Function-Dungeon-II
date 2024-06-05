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
        [SerializeField] private PathData pathData;

        private Transform _gridTileTransform;

        private void Awake()
        {
            _gridTileTransform = gridTile.transform;
        }

        private void Start()
        {
            for (var i = 0; i < pathData.XGridSize; i++)
            {
                for (var j = 0; j < pathData.YGridSize; j++)
                {
                    switch (pathData.generatedGrid[i, j])
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

