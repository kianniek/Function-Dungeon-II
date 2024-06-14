using Unity.AI.Navigation;
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
        [SerializeField] private NavMeshSurface navMeshSurface;

        private Transform _gridTileTransform;

        public Vector3 PathStartPosition => new Vector3(gridData.PathStartIndex.x * _gridTileTransform.localScale.x, 1, gridData.PathStartIndex.y * _gridTileTransform.localScale.y);
        public Vector3 PathEndPosition => new Vector3(gridData.PathEndIndex.x * _gridTileTransform.localScale.x, 1, gridData.PathEndIndex.y * _gridTileTransform.localScale.y);

        private void Awake()
        {
            _gridTileTransform = gridTile.transform;
        }

        /// <summary>
        /// Instantiates the right gridtiles at the postions defined in PathData, determines start and endposition of path
        /// </summary>
        private void Start()
        {
            for (var i = 0; i < gridData.XGridSize; i++)
            {
                for (var j = 0; j < gridData.YGridSize; j++)
                {
                    switch (gridData.generatedGrid[i, j])
                    {
                        //2D
                        //case (GridTileTypes.Empty):
                        //    Instantiate(gridTile, new Vector3(i * _gridTileTransform.localScale.x, j * _gridTileTransform.localScale.y, 0), Quaternion.identity, transform);
                        //    break;
                        //case (GridTileTypes.Placeable):
                        //    Instantiate(placeableGridTile, new Vector3(i * _gridTileTransform.localScale.x, j * _gridTileTransform.localScale.y, 0), Quaternion.identity, transform);
                        //    break;
                        //case (GridTileTypes.Path):
                        //    Instantiate(pathGridTile, new Vector3(i * _gridTileTransform.localScale.x, j * _gridTileTransform.localScale.y, 0), Quaternion.identity, transform);
                        //    break;

                        //3D
                        case (GridTileTypes.Empty):
                            Instantiate(gridTile, new Vector3(i * _gridTileTransform.localScale.x, 0, j * _gridTileTransform.localScale.y), Quaternion.identity, transform);
                            break;
                        case (GridTileTypes.Placeable):
                            Instantiate(placeableGridTile, new Vector3(i * _gridTileTransform.localScale.x, 0, j * _gridTileTransform.localScale.y), Quaternion.identity, transform);
                            break;
                        case (GridTileTypes.Path):
                            Instantiate(pathGridTile, new Vector3(i * _gridTileTransform.localScale.x, 0, j * _gridTileTransform.localScale.y), Quaternion.identity, transform);
                            break;
                    }
                }
            }

            navMeshSurface.BuildNavMesh();
        }
    }
}

