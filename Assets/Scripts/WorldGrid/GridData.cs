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
        private readonly Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        [Tooltip("The indices of all pathcells for a level")]
        [SerializeField] private List<Vector2Int> pathIndices;

        [Tooltip("The size of a grid")]
        [SerializeField] private int xGridSize;
        [SerializeField] private int yGridSize;

        private GridTileTypes[,] gridTileTypes;

        public List<Vector2Int> PathIndices => pathIndices;
        public GridTileTypes[,] generatedGrid => gridTileTypes;
        public int XGridSize => xGridSize;
        public int YGridSize => yGridSize;
        public Vector2Int PathStartIndex => pathIndices[0];
        public Vector2Int PathEndIndex => pathIndices[pathIndices.Count - 1];   


        private void OnValidate()
        {
            gridTileTypes = new GridTileTypes[xGridSize, yGridSize];
            MarkPaths();
            MarkPlaceable();
        }

        /// <summary>
        /// Marks all path coordinates as path
        /// </summary>
        private void MarkPaths()
        {
            for (var i = 0; i < xGridSize; i++)
            {
                for (var j = 0; j < yGridSize; j++)
                {
                    if (pathIndices.Contains(new Vector2Int(i, j)))
                    {
                        gridTileTypes[i, j] = GridTileTypes.Path;
                    }
                }
            }
        }

        /// <summary>
        /// Marks cells around the paths (which arent paths) as placeable
        /// </summary>
        private void MarkPlaceable()
        {
            foreach (var pathIndex in pathIndices)
            {
                foreach (var direction in directions)
                {
                    var tileToCheck = pathIndex + direction;

                    if (tileToCheck.x < 0 || tileToCheck.x > xGridSize - 1 || tileToCheck.y < 0 || tileToCheck.y > yGridSize - 1)
                        continue;

                    if (gridTileTypes[tileToCheck.x, tileToCheck.y] == GridTileTypes.Path)
                        continue;

                    gridTileTypes[tileToCheck.x, tileToCheck.y] = GridTileTypes.Placeable;
                }
            }
        }
    }
}