using TMPro;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject defaultPanel;

        [Header("Title Text")]
        [SerializeField] private TMP_Text stateTextObject;
        [SerializeField] private string pausedText = "Paused";

        /// <summary>
        /// Resumes the game
        /// </summary>
        public void Resume()
        {
            Time.timeScale = 1;
        }

        /// <summary>
        /// Pauses the game and shows the pause menu
        /// </summary>
        public void Pause()
        {
            defaultPanel.SetActive(true);
            stateTextObject.text = pausedText;

            Time.timeScale = 0;
        }

        /// <summary>
        /// Function to toggle a menu GameObject
        /// </summary>
        /// <param name="menu"></param>
        public void ToggleMenu(GameObject menu)
        {
            if (menu)
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

        public void Reset()
        {
            defaultPanel.SetActive(false);

            Time.timeScale = 1;
        }
    }
}
