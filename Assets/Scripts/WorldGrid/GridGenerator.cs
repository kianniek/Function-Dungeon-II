using UnityEngine;

namespace WorldGrid
{
    /// <summary>
    /// Used to quickly generate a grid 
    /// </summary>
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject gridTile;
        [SerializeField] private int xSize = 20;
        [SerializeField] private int ySize = 20;
        [SerializeField] private bool generate;
        [SerializeField] private Material pathMaterial;
        [SerializeField] private Material availableMaterial;
        [SerializeField] private PathData pathData;

        private void Start()
        {
            if (generate)
            {
                for (var i = 0; i < xSize; i++)
                {
                    for (var j = 0; j < ySize; j++)
                    {
                        GameObject tile = Instantiate(gridTile, new Vector3(i, j, 0), Quaternion.identity, transform);
                        tile.name = $"GridTile({i},{j})";

                        for (var k = 0; k < pathData.PathCoordinates.Count; k++)
                        {
                            var currentPathTile = new Vector2(i, j);
                            if (pathData.PathCoordinates[k] == currentPathTile)
                            {
                                tile.AddComponent<PathTile>();
                                tile.GetComponent<SpriteRenderer>().material = pathMaterial;
                            }
                            CheckNeighbourTiles(pathData.PathCoordinates[k] + new Vector2(-1, 0), currentPathTile, tile);
                            CheckNeighbourTiles(pathData.PathCoordinates[k] + new Vector2(+1, 0), currentPathTile, tile);
                            CheckNeighbourTiles(pathData.PathCoordinates[k] + new Vector2(0, -1), currentPathTile, tile);
                            CheckNeighbourTiles(pathData.PathCoordinates[k] + new Vector2(0, +1), currentPathTile, tile);
                        }
                    }
                }
            }
        }

        private void CheckNeighbourTiles(Vector2 pathTileToCheck, Vector2 currentPathTile, GameObject tile)
        {
            if (pathTileToCheck == currentPathTile)
            {
                if (!pathData.PathCoordinates.Contains(pathTileToCheck))
                {
                    tile.AddComponent<PlaceableTile>();
                    tile.GetComponent<SpriteRenderer>().material = availableMaterial;
                }
            }
        }
    }
}

