using System.Collections;
using System.Collections.Generic;
using Progression;
using Progression.Grading;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LevelSelect
{
    public class LevelSelectProgression : MonoBehaviour
    {
        [SerializeField] private GameProgressionData gameProgressionContainer;
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
        public void CheckGameProgression()
        {
            for (var i = 0; i < sceneNamesOfLevels.Length; i++)
            {
                var previousSceneName = i > 0 ? sceneNamesOfLevels[i - 1] : null;
                var levelButton = levelButtons[i];
                levelButton.interactable = false;

                // Check if the level has been played before
                if (i == 0)
                    levelButton.interactable = true;
                else if (gameProgressionContainer.TryGetLevelData(previousSceneName, out var levelProgression))
                {
                    if (levelProgression.GetLatestLevelData().Grade == passingGradeContainer)
                            levelButtons[i].interactable = true;
                    else
                            levelButtons[i].interactable = false;
                }
            }
        }

        private void OnEnable()
        {
            CheckGameProgression();
        }
    }
}