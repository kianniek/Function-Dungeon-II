using System.Collections.Generic;
using UnityEngine;

namespace WorldGrid
{
    //TODO what algorithm
    [CreateAssetMenu(fileName = "PathData", menuName = "WorldGrid/PathData", order = 0)]
    public class PathData : ScriptableObject
    {
        private readonly Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        [Tooltip("The coordinates of all pathcells for a level")]
        [SerializeField] private List<Vector2Int> pathCoordinates;

        [Tooltip("The size of a grid")]
        [SerializeField] private int xGridSize;
        [SerializeField] private int yGridSize;

        private GridTileTypes[,] gridTileTypes;

        public List<Vector2Int> PathCoordinates => pathCoordinates;
        public GridTileTypes[,] generatedGrid => gridTileTypes;
        public int XGridSize => xGridSize;
        public int YGridSize => yGridSize;

        private void OnValidate()
        {
            gridTileTypes = new GridTileTypes[xGridSize, yGridSize];
            MarkPaths();
            MarkPlaceable();
        }

        private void MarkPaths()
        {
            for (var i = 0; i < xGridSize; i++)
            {
                for (var j = 0; j < yGridSize; j++)
                {
                    if (pathCoordinates.Contains(new Vector2Int(i, j)))
                    {
                        gridTileTypes[i, j] = GridTileTypes.Path;
                    }
                }
            }
        }

        private void MarkPlaceable()
        {
            foreach (var pathIndex in pathCoordinates)
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