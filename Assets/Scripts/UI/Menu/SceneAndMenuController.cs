using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu
{
    public class SceneAndMenuController : MonoBehaviour
    {
        /// <summary>
        /// Function to quit the game
        /// </summary>
        public void QuitGame()
        {
            Debug.Log("Quit Game requested");

            Application.Quit();
        }

        /// <summary>
        /// Function to switch scenes by scene name
        /// </summary>
        /// <param name="sceneName"></param>
        public void SwitchSceneByName(string sceneName)
        {
            Debug.Log("Switching to scene: " + sceneName);

            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// Function to switch scenes by scene index
        /// </summary>
        /// <param name="sceneIndex"></param>
        public void SwitchSceneByIndex(int sceneIndex)
        {
            Debug.Log("Switching to scene index: " + sceneIndex);

            SceneManager.LoadScene(sceneIndex);
        }

        /// <summary>
        /// Function to go to the next scene and wrap around to the first scene if it goes past the last scene
        /// </summary>
        public void NextScene()
        {
            // Current scene index
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Calculate next scene index
            var nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

            // Log message to console
            Debug.Log("Switching to next scene index: " + nextSceneIndex);

            // Load next scene by calculated index
            SceneManager.LoadScene(nextSceneIndex);
        }

        /// <summary>
        /// Function to toggle a menu GameObject
        /// </summary>
        /// <param name="menu"></param>
        public void ToggleMenu(GameObject menu)
        {
            if (menu != null)
            {
                menu.SetActive(!menu.activeSelf);

                // Log the new state
                Debug.Log("Menu " + (menu.activeSelf ? "opened" : "closed"));
            }
            else
            {
                Debug.LogError("Menu GameObject is null. Please add a Menu Gameobject");
            }
        }
    }
}