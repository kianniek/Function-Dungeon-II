using System.Collections.Generic;
using UnityEngine;

namespace WorldGrid
{
    /// <summary>
    /// Holds griddata and marks path and placeable tiles
    /// </summary>
    [CreateAssetMenu(fileName = "GridData", menuName = "WorldGrid/GridData", order = 0)]
    public class GridData : ScriptableObject
    {
        private readonly IReadOnlyList<Vector2Int> _directions =
            new[] { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        [Tooltip("The indices of all path cells for a level")] [SerializeField]
        private List<Vector2Int> pathIndices;

        [Tooltip("The size of a grid")] [SerializeField]
        private int xGridSize;

        [SerializeField] private int yGridSize;

        private GridTileTypes[,] _gridTileTypes;

        public IReadOnlyList<Vector2Int> PathIndices => pathIndices;

        public GridTileTypes[,] GeneratedGrid => _gridTileTypes;

        public int XGridSize => xGridSize;

        public int YGridSize => yGridSize;

        public Vector2Int PathStartIndex => pathIndices[0];

        public Vector2Int PathEndIndex => pathIndices[^1];

        private void OnValidate()
        {
            _gridTileTypes = new GridTileTypes[xGridSize, yGridSize];

            MarkPaths();
            MarkPlaceable();
        }

        // Marks all path coordinates as path
        private void MarkPaths()
        {
            for (var i = 0; i < xGridSize; i++)
            {
                for (var j = 0; j < yGridSize; j++)
                {
                    if (pathIndices.Contains(new Vector2Int(i, j)))
                    {
                        _gridTileTypes[i, j] = GridTileTypes.Path;
                    }
                }
            }
        }

        // Marks cells around the paths (which aren't paths) as placeable
        private void MarkPlaceable()
        {
            foreach (var pathIndex in pathIndices)
            {
                foreach (var direction in _directions)
                {
                    var tileToCheck = pathIndex + direction;

                    if (
                        tileToCheck.x < 0 ||
                        tileToCheck.x > xGridSize - 1 ||
                        tileToCheck.y < 0 ||
                        tileToCheck.y > yGridSize - 1
                    ) continue;

                    if (_gridTileTypes[tileToCheck.x, tileToCheck.y] != GridTileTypes.Path)
                    {
                        _gridTileTypes[tileToCheck.x, tileToCheck.y] = GridTileTypes.Placeable;
                    }
                }
            }
        }
    }
}