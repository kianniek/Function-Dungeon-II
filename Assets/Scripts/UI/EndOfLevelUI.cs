using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EndOfLevelUI : MonoBehaviour
    {
        private Scene _activeScene;

        private int _levelSelectionScreenBuildIndex;

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
            SceneManager.LoadScene(_levelSelectionScreenBuildIndex);
        }

        /// <summary>
        /// Navigates to next level when next level button is clicked
        /// </summary>
        public void NextLevelButtonClicked()
        {
            //TODO needs to be implemented waiting for dependency
            SceneManager.LoadScene(_activeScene.buildIndex + 1);
        }
    }
}
