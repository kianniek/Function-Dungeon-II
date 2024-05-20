using Events.GameEvents.Typed;
using Progression.Grading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuOverlay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject defaultPanel;
        [SerializeField] private Image gradeSprite;
        [SerializeField] private GameObject nextLevelButton;
        [SerializeField] private GameObject returnToGameButton;
        
        [Header("Title Text")]
        [SerializeField] private TMP_Text stateTextObject;
        [SerializeField] private string clearedText = "Level Cleared!";
        [SerializeField] private string failedText = "Level Failed!";
        [SerializeField] private string pausedText = "Paused";
        
        [Header("Score Text")]
        [SerializeField] private TMP_Text scoreTextObject;
        [SerializeField] private string scoreTextFormat = "Score: {0}";
        
        [Header("Events")] 
        [SerializeField] private IntGameEvent onScoreChanged;
        [SerializeField] private GradeGameEvent onGradeChanged;
        
        private void OnEnable()
        {
            onScoreChanged?.AddListener(UpdateScoreText);
            onGradeChanged?.AddListener(UpdateGradeSprite);
        }
        
        private void OnDisable()
        {
            onScoreChanged?.RemoveListener(UpdateScoreText);
            onGradeChanged?.RemoveListener(UpdateGradeSprite);
        }
        
        private void UpdateScoreText(int score)
        {
            scoreTextObject.text = string.Format(scoreTextFormat, score);
        }
        
        private void UpdateGradeSprite(Grade grade)
        {
            gradeSprite.sprite = grade.Icon;
        }
        
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
        
        /// <summary>
        /// Shows the level cleared screen
        /// </summary>
        public void LevelCleared()
        {
            defaultPanel.SetActive(true);
            stateTextObject.text = clearedText;
            nextLevelButton.SetActive(true);
        }
        
        /// <summary>
        /// Shows the level failed screen
        /// </summary>
        public void LevelFailed()
        {
            defaultPanel.SetActive(true);
            stateTextObject.text = failedText;
        }
        
        public void Reset()
        {
            defaultPanel.SetActive(false);
            nextLevelButton.SetActive(false);
            returnToGameButton.SetActive(false);
            
            Time.timeScale = 1;
        }
    }
}