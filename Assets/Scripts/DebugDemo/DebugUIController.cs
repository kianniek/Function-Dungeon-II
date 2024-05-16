using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

namespace DebugDemo
{
    public class DebugUIController : MonoBehaviour
    {
        public static DebugUIController Instance { get; private set; }

        private TextMeshProUGUI _debugText;
        private bool _isDebugUIVisible = true;
        private int _totalScenes;
        [SerializeField] private InputAction SceneSwitch1DAction;
        [SerializeField] private InputAction DisableAction;
        [SerializeField] private InputAction QuitAction;
        [SerializeField] private InputAction ReloadAction;

        private int _currentSceneIndex;
        private List<int> _loadedSceneIndexes = new List<int>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetupUI();
        }

        private void Start()
        {
            _totalScenes = SceneManager.sceneCountInBuildSettings;
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            StartCoroutine(LoadAllScenes());
        }

        private void OnEnable()
        {
            SceneSwitch1DAction.Enable();
            SceneSwitch1DAction.performed += OnSceneSwitch;

            DisableAction.Enable();
            DisableAction.performed += OnDisable;

            QuitAction.Enable();
            QuitAction.performed += OnQuit;

            ReloadAction.Enable();
            ReloadAction.performed += ReloadScene;
        }

        private void OnDisable()
        {
            SceneSwitch1DAction.Disable();
            SceneSwitch1DAction.performed -= OnSceneSwitch;

            DisableAction.Disable();
            DisableAction.performed -= OnDisable;

            QuitAction.Disable();
            QuitAction.performed -= OnQuit;

            ReloadAction.Disable();
            ReloadAction.performed -= ReloadScene;
        }

        private void OnSceneSwitch(InputAction.CallbackContext context)
        {
            float direction = context.ReadValue<float>();
            SwitchScene(direction);
        }

        private void OnDisable(InputAction.CallbackContext context)
        {
            ToggleDebugUI();
        }

        private void OnQuit(InputAction.CallbackContext context)
        {
            SwitchSceneToIndex(0);
        }

        private IEnumerator LoadAllScenes()
        {
            for (int i = 0; i < _totalScenes; i++)
            {
                if (i != _currentSceneIndex)
                {
                    if (!IsSceneLoaded(i))
                    {
                        var asyncLoad = SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
                        asyncLoad.allowSceneActivation = true;
                        while (!asyncLoad.isDone)
                        {
                            UpdateDebugText($"Loading Scene {i}...");
                            yield return null;
                        }
                        Scene scene = SceneManager.GetSceneByBuildIndex(i);
                        _loadedSceneIndexes.Add(i);
                        UpdateDebugText($"Disabling Scene {i} objects");
                        foreach (GameObject go in scene.GetRootGameObjects())
                        {
                            go.SetActive(false);
                        }
                        UpdateDebugText($"DONE Disabling Scene {i} objects");
                    }
                }
            }
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(_currentSceneIndex));
            UpdateDebugText("All scenes loaded.");
        }

        private void SwitchScene(float direction)
        {
            UpdateDebugText($"Switching scene");

            int nextSceneIndex = direction > 0 ? (_currentSceneIndex + 1) % _totalScenes : (_currentSceneIndex - 1 + _totalScenes) % _totalScenes;
            SwitchSceneToIndex(nextSceneIndex);
        }

        private void SwitchSceneToIndex(int sceneIndex)
        {
            UpdateDebugText($"Start Scene switch to scene with index {sceneIndex}");

            if (!IsSceneLoaded(sceneIndex))
            {
                StartCoroutine(LoadSceneAndSwitch(sceneIndex));
            }
            else
            {
                StartCoroutine(SwitchToScene(sceneIndex));
            }
        }

        private IEnumerator LoadSceneAndSwitch(int sceneIndex)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = true;
            while (!asyncLoad.isDone)
            {
                UpdateDebugText($"Loading Scene {sceneIndex}...");
                yield return null;
            }

            Scene scene = SceneManager.GetSceneByBuildIndex(sceneIndex);
            _loadedSceneIndexes.Add(sceneIndex);

            foreach (GameObject go in scene.GetRootGameObjects())
            {
                go.SetActive(false);
            }

            StartCoroutine(SwitchToScene(sceneIndex));
        }

        private IEnumerator SwitchToScene(int sceneIndex)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
            UpdateDebugText($"Switching to scene {sceneIndex}...");

            foreach (GameObject go in SceneManager.GetSceneByBuildIndex(_currentSceneIndex).GetRootGameObjects())
            {
                go.SetActive(false);
            }

            foreach (GameObject go in SceneManager.GetSceneByBuildIndex(sceneIndex).GetRootGameObjects())
            {
                go.SetActive(true);
            }

            _currentSceneIndex = sceneIndex;
            UpdateDebugText($"Switched to scene {sceneIndex}");

            yield return null;
        }

        private void ToggleDebugUI()
        {
            _isDebugUIVisible = !_isDebugUIVisible;
            _debugText.gameObject.SetActive(_isDebugUIVisible);
        }

        private void UpdateDebugText(string message)
        {
            _debugText.text = message;
        }

        private void ReloadScene(InputAction.CallbackContext context)
        {
            StartCoroutine(ReloadCurrentScene());
        }

        private IEnumerator ReloadCurrentScene()
        {
            UpdateDebugText($"Reloading current scene...");

            // Pause physics simulation
            Time.timeScale = 0;
            // Save the currently active scene index
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Unload all scenes except the current one
            foreach (int sceneIndex in _loadedSceneIndexes)
            {
                if (sceneIndex != currentSceneIndex)
                {
                    yield return SceneManager.UnloadSceneAsync(sceneIndex);
                }
            }

            _loadedSceneIndexes.RemoveAll(index => index != currentSceneIndex);

            // Reload the current scene
            yield return SceneManager.LoadSceneAsync(currentSceneIndex);

            // Reload all other scenes
            yield return StartCoroutine(LoadAllScenes());

            // Resume physics simulation
            Time.timeScale = 1;

            UpdateDebugText($"Reloaded current scene");
        }

        private bool IsSceneLoaded(int buildIndex)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).buildIndex == buildIndex)
                {
                    return true;
                }
            }
            return false;
        }

        private void SetupUI()
        {
            // Create Canvas
            GameObject canvasObject = new GameObject("DebugUICanvas");
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.sortingOrder = 999;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasObject.AddComponent<GraphicRaycaster>();

            DontDestroyOnLoad(canvasObject);

            // Create Text
            GameObject textObject = new GameObject("DebugText");
            textObject.transform.SetParent(canvasObject.transform);
            _debugText = textObject.AddComponent<TextMeshProUGUI>();
            _debugText.fontSize = 8;
            _debugText.alignment = TextAlignmentOptions.BottomLeft;
            _debugText.color = Color.gray;
            RectTransform textRect = _debugText.GetComponent<RectTransform>();
            textRect.anchorMin = new Vector2(0, 0);
            textRect.anchorMax = new Vector2(0, 0);
            textRect.pivot = new Vector2(0, 0);
            textRect.anchoredPosition = new Vector2(10, 10); // Adjust the offset as needed
        }
    }
}