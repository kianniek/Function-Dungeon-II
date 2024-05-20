using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Progression.Grading
{
    /// <summary>
    /// Represents a level grading system.
    /// </summary>
    [CreateAssetMenu(fileName = "Level Grading System", menuName = "Progression/Grading/Level Grading System")]
    public class LevelGradingSystem : ScriptableObject
    {
        [SerializeField] private List<LevelGradingEntry> gradingSystem = new();
        
        /// <summary>
        /// The default grade to return if no grade is found.
        /// </summary>
        public Grade DefaultGrade => gradingSystem[^1].Grade;
        
        /// <summary>
        /// Calculate the grade for a given score.
        /// </summary>
        /// <param name="score"> The score to calculate the grade for. </param>
        /// <returns> The grade for the given score. </returns>
        public Grade CalculateGrade(int score)
        {
            // Iterate through the sorted list and find the appropriate grade
            foreach (var gradeContainer in gradingSystem.Where(
                         gradeContainer => score >= gradeContainer.ScoreThreshold
                     ))
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