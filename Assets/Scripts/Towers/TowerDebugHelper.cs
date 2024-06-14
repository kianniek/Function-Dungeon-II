using UnityEngine;

namespace Towers
{
    /// <summary>
    /// Visualize range of towers (for debug purposes)
    /// </summary>
    public class TowerDebugHelper : MonoBehaviour
    {
        [SerializeField] private TowerVariables towerVariables;
        [SerializeField] private Transform rangeTransform;

        /// <summary>
        /// Visualizes range
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(rangeTransform.position, towerVariables.Range / 2);
        }
    }
}
