using System;
using UnityEngine;

namespace Progression.Grading
{
    /// <summary>
    /// Represents a single entry of the per level grading settings.
    /// </summary>
    [Serializable]
    public class LevelGradingSettingsEntry
    {
        [SerializeField] private string levelName; 
        [SerializeField] private LevelGradingSystem gradingSystem;
        
        /// <summary>
        /// The name of the level the associated grading system is for.
        /// </summary>
        public string LevelName => levelName;
        
        /// <summary>
        /// The grading system for the level.
        /// </summary>
        public LevelGradingSystem GradingSystem => gradingSystem;
    }
}