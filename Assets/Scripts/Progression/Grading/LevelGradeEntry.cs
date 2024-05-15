using System;
using UnityEngine;

namespace Progression.Grading
{
    [Serializable]
    public class LevelGradeEntry : IComparable<LevelGradeEntry>
    {
        [SerializeField, Min(0)] private int scoreThreshold;
        [SerializeField] private Grade grade;
        
        public int ScoreThreshold => scoreThreshold;
        
        public Grade Grade => grade;
        
        public int CompareTo(LevelGradeEntry other)
        {
            return scoreThreshold.CompareTo(other.ScoreThreshold);
        }
    }
}