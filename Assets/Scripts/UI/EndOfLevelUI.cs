using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EndOfLevelUI : MonoBehaviour
    {
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
            SceneManager.LoadScene(_activeScene.name);//name or buildindex better?
        }

        /// <summary>
        /// Navigates to level selection scene when level select button clicked
        /// </summary>
        public void LevelSelectButtonClicked()
        {
            //TODO needs to be implemented waiting for dependency
            Debug.Log("Works");
        }
    }
}
