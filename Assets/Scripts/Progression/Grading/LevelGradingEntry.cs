using System;
using UnityEngine;

namespace Progression.Grading
{
    /// <summary>
    /// Represents a single entry in a level grading system.
    /// </summary>
    [Serializable]
    public class LevelGradingEntry : IComparable<LevelGradingEntry>
    {
        [SerializeField, Min(0)] private int scoreThreshold;
        [SerializeField] private Grade grade;
        
        /// <summary>
        /// The score threshold for this grading entry.
        /// </summary>
        public int ScoreThreshold => scoreThreshold;
        
        /// <summary>
        /// The grade for this grading entry.
        /// </summary>
        public Grade Grade => grade;
        
        public int CompareTo(LevelGradingEntry other)
        {
            return scoreThreshold.CompareTo(other.ScoreThreshold);
        }
    }
}