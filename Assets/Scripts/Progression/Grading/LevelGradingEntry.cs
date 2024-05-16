using System;
using UnityEngine;

namespace Progression.Grading
{
    [Serializable]
    public class LevelGradingEntry : IComparable<LevelGradingEntry>
    {
        [SerializeField, Min(0)] private int scoreThreshold;
        [SerializeField] private Grade grade;
        
        public int ScoreThreshold => scoreThreshold;
        
        public Grade Grade => grade;
        
        public int CompareTo(LevelGradingEntry other)
        {
            return scoreThreshold.CompareTo(other.ScoreThreshold);
        }
    }
}