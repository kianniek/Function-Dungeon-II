using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private float switchDelay;
    
    public void SwitchScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }
    
    public void SwitchSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    
    private IEnumerator LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(switchDelay);
        SceneManager.LoadScene(sceneName);
    }
    
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return new WaitForSeconds(switchDelay);
        
        SceneManager.LoadSceneAsync(sceneName);
        
        //if scene has not finished loading, wait
        while (!SceneManager.GetSceneByName(sceneName).isLoaded)
            yield return null;
        
        //if scene has finished loading, activate it
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
    }
    
    public void ReloadScene()
    {
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
    }
}