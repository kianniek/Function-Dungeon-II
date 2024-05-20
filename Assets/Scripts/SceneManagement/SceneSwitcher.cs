using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] private string sceneName;
        [SerializeField] private float switchDelay;
        public void SwitchScene()
        {
            StartCoroutine(LoadScene(sceneName));
        }

        public void SwitchSceneAsync()
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return new WaitForSeconds(switchDelay);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            yield return new WaitForSeconds(switchDelay);
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

            //if scene has not finished loading, wait
            while (!UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                yield return null;
            }

            //if scene has finished loading, activate it
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName));
        }
    }
}