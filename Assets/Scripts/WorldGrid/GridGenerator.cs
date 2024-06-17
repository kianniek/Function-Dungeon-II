using System;
using Attributes;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.Events;

namespace WorldGrid
{
    /// <summary>
    /// Used to quickly generate a grid 
    /// </summary>
    public class GridGenerator : MonoBehaviour
    {
        [Header("Tile Prefabs")] 
        [SerializeField] private GridTile gridTile;
        [SerializeField] private PlaceableTile placeableGridTile;
        [SerializeField] private PathTile pathGridTile;

        [Header("World Design")] 
        [SerializeField, Expandable] private GridData gridData;

        [Header("Navigation Settings")] 
        [SerializeField] private NavMeshSurface navMeshSurface;

        private Transform _gridTileTransform;

        public Vector3 PathStartPosition => GetPositionFromIndex(gridData.PathStartIndex);

        public Vector3 PathEndPosition => GetPositionFromIndex(gridData.PathEndIndex);

        private void Awake()
        {
            _gridTileTransform = gridTile.transform;
        }

        private void Start()
        {
            // Instantiates the right grid tiles at the positions defined in PathData,
            // determines start and end position of path
            for (var i = 0; i < gridData.XGridSize; i++)
            {
                for (var j = 0; j < gridData.YGridSize; j++)
                {
                    switch (gridData.GeneratedGrid[i, j])
                    {
                        case GridTileTypes.Empty:
                            CreateTile(i, j, gridTile);
                            break;
                        case GridTileTypes.Placeable:
                            CreateTile(i, j, placeableGridTile);
                            break;
                        case GridTileTypes.Path:
                            CreateTile(i, j, pathGridTile);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            navMeshSurface.BuildNavMesh();
        }

        private void CreateTile(int i, int j, MonoBehaviour tile)
        {
            Instantiate(tile, GetPositionFromIndex(i, j), Quaternion.identity, transform);
        }

        private Vector3 GetPositionFromIndex(Vector2Int index)
        {
            return GetPositionFromIndex(index.x, index.y);
        }
        
        private Vector3 GetPositionFromIndex(int x, int y)
        {
            return new Vector3
            {
                x = x * _gridTileTransform.localScale.x + transform.position.x,
                y = 0,
                z = y * _gridTileTransform.localScale.y + transform.position.z
            };
        }
    }
}