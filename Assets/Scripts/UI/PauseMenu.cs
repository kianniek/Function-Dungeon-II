using TMPro;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject defaultPanel;
        [SerializeField] private GameObject returnToGameButton;

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
            returnToGameButton.SetActive(true);
            stateTextObject.text = pausedText;

            Time.timeScale = 0;
        }
        public void Reset()
        {
            defaultPanel.SetActive(false);
            returnToGameButton.SetActive(false);

            Time.timeScale = 1;
        }
    }
}
