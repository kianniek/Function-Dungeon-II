using UnityEngine;

namespace WorldGrid
{
    /// <summary>
    /// Used to quickly generate a grid 
    /// </summary>
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject gridTile;
        [SerializeField] private int xTileSize = 1;
        [SerializeField] private int yTileSize = 1;
        [SerializeField] private int xGridSize = 20;
        [SerializeField] private int yGridSize = 20;
        [SerializeField] private Material pathMaterial;
        [SerializeField] private Material availableMaterial;
        [SerializeField] private PathData pathData;

        private Vector2[] _directions = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

        private void Start()
        {
            for (var i = 0; i < xGridSize; i += xTileSize)
            {
                for (var j = 0; j < yGridSize; j += yTileSize)
                {
                    GameObject tile = Instantiate(gridTile, new Vector3(i, j, 0), Quaternion.identity, transform);
                    tile.name = $"GridTile({i},{j})";
                    var currentTile = new Vector2(i, j);

                    for (var k = 0; k < pathData.PathCoordinates.Count; k++)
                    {

                        if (pathData.PathCoordinates[k] == currentTile)
                        {
                            tile.AddComponent<PathTile>();
                            tile.GetComponent<SpriteRenderer>().material = pathMaterial;
                        }

                        foreach (var direction in _directions)
                        {
                            IsPlaceableTile(pathData.PathCoordinates[k] + direction, currentTile, tile);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if currentTile is an placeable tile by checking if the cell is next to an path and if its not an path
        /// </summary>
        /// <param name="pathTileToCheck">The pathcell you want to check</param>
        /// <param name="currentPathTile">The current path tile of the grid</param>
        /// <param name="tile">The current path tile gameobject</param>
        private void IsPlaceableTile(Vector2 pathTileToCheck, Vector2 currentPathTile, GameObject tile)
        {
            if (pathTileToCheck == currentPathTile && !pathData.PathCoordinates.Contains(pathTileToCheck))
            {
                tile.AddComponent<PlaceableTile>();
                tile.GetComponent<SpriteRenderer>().material = availableMaterial;
            }
        }
    }
}

