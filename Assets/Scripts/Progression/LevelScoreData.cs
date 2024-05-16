using System;
using Progression.Grading;

namespace Progression
{
    /// <summary>
    /// Record for storing the score and grade of a level.
    /// </summary>
    [Serializable]
    public record LevelScoreData(int Score, Grade Grade) : IComparable<LevelScoreData>
    {
        /// <summary>
        /// The score of the level.
        /// </summary>
        public int Score { get; } = Score;
        
        /// <summary>
        /// The grade of the level.
        /// </summary>
        public Grade Grade { get; } = Grade;
        
        public int CompareTo(LevelScoreData other)
        {
            return Score.CompareTo(other.Score);
        }
    }
}