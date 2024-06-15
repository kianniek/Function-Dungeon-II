using UnityEngine;
using UnityEngine.AI;

namespace ObjectMovement
{
    /// <summary>
    /// Class which handles mushroom of bloom movement.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class BloomShroomMovement : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        public void SetAndStartPath(Vector3 pathEndPosition)
        {
            _navMeshAgent.SetDestination(pathEndPosition);
        }
    }
}