using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Progression.Grading
{
    [CreateAssetMenu(fileName = "Level Grading System", menuName = "Progression/Grading/Level Grading System")]
    public class LevelGradingSystem : ScriptableObject
    {
        [SerializeField] private List<LevelGradingEntry> gradingSystem = new();
        
        /// <summary>
        /// 
        /// </summary>
        public Grade DefaultGrade => gradingSystem[^1].Grade;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public Grade CalculateGrade(int score)
        {
            // Iterate through the sorted list and find the appropriate grade
            foreach (var gradeContainer in
                     gradingSystem.Where(gradeContainer => score >= gradeContainer.ScoreThreshold))
            {
                return gradeContainer.Grade;
            }
            
            // If no grade is found, return the last (base) grade in the list
            return DefaultGrade;
        }
        
        // Sort the grading system by score threshold in descending order
        private void OnValidate()
        {
            gradingSystem.Sort();
            gradingSystem.Reverse();
        }
    }
}