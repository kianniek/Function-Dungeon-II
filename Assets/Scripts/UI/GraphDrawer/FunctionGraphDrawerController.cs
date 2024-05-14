using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.GraphDrawer
{
    public class FunctionGraphDrawerController : MonoBehaviour
    {
        //get canvas in awake and set it to an rendermode screen space camrea and attach the camera.main to it
        private void Awake()
        {
            var canvas = GetComponentInParent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
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
