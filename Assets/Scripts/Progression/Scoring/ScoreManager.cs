using Attributes;
using GameEvent.Events.Typed;
using Progression.Grading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Progression.Scoring
{
    [CreateAssetMenu(fileName = "ScoreManager", menuName = "Progression/Score Manager", order = 0)]
    public class ScoreManager : ScriptableObject
    {
        [Header("Game Data")] 
        [SerializeField] private GameProgressionData gameProgressionContainer;
        
        [Header("Score & Grading Settings")] 
        [SerializeField, Expandable] private LevelGradingSystem gradingSystem;
        [SerializeField] private bool allowNegativeScore;
        
        [Header("Events")] 
        [SerializeField] private IntGameEvent onScoreAdd;
        [SerializeField] private IntGameEvent onScoreChanged;
        [SerializeField] private GradeGameEvent onGradeChanged;
        
        public Grade CurrentGrade { get; private set; }
        
        public int CurrentScore { get; private set; }
        
        private void OnEnable()
        {
            SceneManager.activeSceneChanged += ResetScoringSystem;
        }
        
        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= ResetScoringSystem;
        }
        
        private void OnValidate()
        {
            onScoreAdd?.AddListener(UpdateScore);
        }
        
        private void ResetScoringSystem(Scene arg0, Scene arg1)
        {
            CurrentScore = 0;
            
            UpdateGameProgression();
        }
        
        private void UpdateScore(int points)
        {
            var newScore = CurrentScore - points;
            
            if (newScore < 0 && !allowNegativeScore)
            {
                CurrentScore = 0;
                
                UpdateGameProgression();
            }
            else
            {
                CurrentScore = newScore;
            }
        }
        
        private void UpdateGameProgression()
        {
            onScoreChanged?.Invoke(CurrentScore);
            
            CurrentGrade = gradingSystem.CalculateGrade(CurrentScore);
            
            onGradeChanged?.Invoke(CurrentGrade);
            
            gameProgressionContainer.UpdateOrAddLevelScore(
                SceneManager.GetActiveScene().name,
                new LevelScoreData
                {
                    Score = CurrentScore,
                    Grade = CurrentGrade
                }
            );
        }
    }
}