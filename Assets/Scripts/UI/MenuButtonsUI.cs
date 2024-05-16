using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuButtonsUI : MonoBehaviour
    {
        [SerializeField] private GameObject pauzeMenu;
        private Scene _activeScene;

        private void Start()
        {
            _activeScene = SceneManager.GetActiveScene();
        }

        /// <summary>
        /// Reloads scene when retry button clicked
        /// </summary>
        public void RetryButtonClicked()
        {
            SceneManager.LoadScene(_activeScene.name);
        }

        /// <summary>
        /// Navigates to level selection scene when level select button clicked
        /// </summary>
        public void LevelSelectButtonClicked()
        {
            //TODO needs to be implemented waiting for level selection screen
            Debug.Log("Works");
        }

        /// <summary>
        /// Navigates to next level when next level button is clicked
        /// </summary>
        public void NextLevelButtonClicked()
        {
            //TODO needs to be implemented waiting for dependency
            SceneManager.LoadScene(_activeScene.buildIndex + 1);
        }

        /// <summary>
        /// Opens pauze menu when clicked on pauze button
        /// </summary>
        public void PauzeButtonClicked()
        {
            pauzeMenu.SetActive(true);  
        }

        /// <summary>
        /// Closes pauze menu when clicked on resume button
        /// </summary>
        public void ResumeButtonClicked()
        {
            pauzeMenu.SetActive(false);
        }

        /// <summary>
        /// Navigates to settings menu when settings button is clicked
        /// </summary>
        public void OptionsButtonClicked() 
        { 
            //Not yet implemented
            //SceneManager.LoadScene(settings);
        }

        /// <summary>
        /// Quits game on Quitbutton click
        /// </summary>
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
