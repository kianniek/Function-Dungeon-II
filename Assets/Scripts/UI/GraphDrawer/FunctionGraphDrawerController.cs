using UnityEngine;

namespace UI.GraphDrawer
{
    public class FunctionGraphDrawerController : MonoBehaviour
    {
        [SerializeField] private GameObject origin;
        
        private void Start()
        {
            transform.position = origin.transform.position;
        }
        
        /// <summary>
        /// Disables the graph object children.
        /// </summary>
        public void DisableGraphObject()
        {
            // Disable the graph objects children
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Enables the graph objects children.
        /// </summary>
        public void EnableGraphObject()
        {
            // Enable the graph objects children
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
