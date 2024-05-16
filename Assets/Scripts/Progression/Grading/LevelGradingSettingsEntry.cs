using System;
using UnityEngine;

namespace Progression.Grading
{
    [Serializable]
    public class LevelGradingSettingsEntry
    {
        [SerializeField] private string levelName; 
        [SerializeField] private LevelGradingSystem gradingSystem;
        
        public string LevelName => levelName;
        
        public LevelGradingSystem GradingSystem => gradingSystem;
    }
}