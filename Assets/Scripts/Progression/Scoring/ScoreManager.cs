using System;
using System.Collections.Generic;
using Events.GameEvents.Typed;
using Progression.Grading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Progression.Scoring
{
    /// <summary>
    /// Manages the scoring system for the game.
    /// </summary>
    [CreateAssetMenu(fileName = "Score Manager", menuName = "Progression/Score Manager", order = 0), ExecuteAlways]
    public class ScoreManager : ScriptableObject
    {
        [SerializeField] private List<LevelGradingSettingsEntry> gradingSettings;
        
        [Header("Score Settings")] 
        [SerializeField] private bool allowNegativeScore;
        
        [Header("Game Data")] 
        [SerializeField] private GameProgressionData gameProgressionContainer;
        
        [Header("Events")] 
        [SerializeField] private IntGameEvent onUpdateScore;
        [SerializeField] private IntGameEvent onScoreChanged;
        [SerializeField] private GradeGameEvent onGradeChanged;
        
        /// <summary>
        /// The current grade of the player in the active scene.
        /// </summary>
        public Grade CurrentGrade { get; private set; }
        
        /// <summary>
        /// The current score of the player in the active scene.
        /// </summary>
        public int CurrentScore { get; private set; }
        
        private void OnEnable()
        {
            SceneManager.activeSceneChanged += ResetScoringSystem;
            
            onUpdateScore?.AddListener(UpdateScore);
        }
        
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= ResetScoringSystem;
            
            onUpdateScore?.RemoveListener(UpdateScore);
        }
        
        private void ResetScoringSystem(Scene arg0, Scene arg1)
        {
            CurrentScore = 0;
            
            UpdateGameProgression();
        }
        
        private void UpdateScore(int points)
        {
            var newScore = CurrentScore + points;
            
            if (newScore < 0 && allowNegativeScore)
            {
                CurrentScore = 0;
                
                UpdateGameProgression();
            }
            else
            {
                CurrentScore = newScore;
                
                UpdateGameProgression();
            }
        }
        
        private void UpdateGameProgression()
        {
            onScoreChanged?.Invoke(CurrentScore);
            
            var activeSceneName = SceneManager.GetActiveScene().name;
            
            try
            {
                var gradingSystem = gradingSettings.Find(entry => entry.LevelName == activeSceneName).GradingSystem;
                
                CurrentGrade = gradingSystem.CalculateGrade(CurrentScore);
                
                onGradeChanged?.Invoke(CurrentGrade);
                
                gameProgressionContainer.UpdateOrAddLevelScore(
                    activeSceneName,
                    new LevelScoreData(CurrentScore, CurrentGrade)
                );
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error updating game progression: {e.Message}");
            }
        }
    }
}