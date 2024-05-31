using System.Collections.Generic;
using UnityEngine;

namespace WorldGrid
{
    [CreateAssetMenu(fileName = "PathData", menuName = "WorldGrid/PathData", order = 0)]
    public class PathData : ScriptableObject
    {
        [Tooltip("The coordinates of all pathcells for a level")]
        [SerializeField] private List<Vector2> pathCoordinates;

        public List<Vector2> PathCoordinates => pathCoordinates;
    }
}