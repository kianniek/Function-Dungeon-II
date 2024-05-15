using System;
using Progression.Grading;

namespace Progression
{
    [Serializable]
    public class LevelScoreData
    {
        public int Score { get; set; }
        
        public Grade Grade { get; set; }
    }
}