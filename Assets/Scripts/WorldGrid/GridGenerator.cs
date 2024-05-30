using System.Collections.Generic;
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
        [SerializeField] private List<Vector2> pathTileCoordinates;

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

                        for (var k = 0; k < pathTileCoordinates.Count; k++)
                        {
                            var currentPathTile = new Vector2(i, j);
                            if (pathTileCoordinates[k] == currentPathTile)
                            {
                                tile.AddComponent<PathTile>();
                                tile.GetComponent<SpriteRenderer>().material = pathMaterial;
                            }
                            CheckNeighbourTiles(pathTileCoordinates[k] + new Vector2(-1, 0), currentPathTile, tile);
                            CheckNeighbourTiles(pathTileCoordinates[k] + new Vector2(+1, 0), currentPathTile, tile);
                            CheckNeighbourTiles(pathTileCoordinates[k] + new Vector2(0, -1), currentPathTile, tile);
                            CheckNeighbourTiles(pathTileCoordinates[k] + new Vector2(0, +1), currentPathTile, tile);
                        }
                    }
                }
            }
        }

        private void CheckNeighbourTiles(Vector2 pathTileToCheck, Vector2 currentPathTile, GameObject tile)
        {
            if (pathTileToCheck == currentPathTile)
            {
                if (!pathTileCoordinates.Contains(pathTileToCheck))
                {
                    tile.AddComponent<PlaceableTile>();
                    tile.GetComponent<SpriteRenderer>().material = availableMaterial;
                }
            }
        }
    }
}

