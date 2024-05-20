using Progression;
using Progression.Grading;
using Progression.Scoring;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.LevelSelect
{
    public class LevelSelectProgression : MonoBehaviour
    {
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private Grade passingGradeContainer;
        [SerializeField] private string[] sceneNamesOfLevels;
        [SerializeField] private Button[] levelButtons;

        private void Start()
        {
            CheckGameProgression();
        }

        /// <summary>
        /// Checks the game progression data to determine which levels have been played before. And sets the level buttons to active or 
        /// inactive based on the results.
        /// </summary>
        private void CheckGameProgression()
        {
            for (var i = 0; i < sceneNamesOfLevels.Length; i++)
            {
                var previousSceneName = i > 0 ? sceneNamesOfLevels[i - 1] : null;
                var levelButton = levelButtons[i];
                levelButton.interactable = false;

                // Check if the level has been played before
                if (i == 0)
                    levelButton.interactable = true;
                else if (scoreManager.GameProgressionContainer.TryGetLevelData(previousSceneName, out var levelProgression))
                {
                    levelButtons[i].interactable = levelProgression.GetLatestLevelData().Grade == passingGradeContainer;
                }
            }
        }

        private void OnEnable()
        {
            CheckGameProgression();
        }
    }
}