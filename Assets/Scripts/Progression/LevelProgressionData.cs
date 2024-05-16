using System;
using System.Collections.Generic;
using System.Linq;

namespace Progression
{
    /// <summary>
    /// A class that holds the progression data of a level.
    /// </summary>
    [Serializable]
    public class LevelProgressionData
    {
        private readonly List<LevelScoreData> _levelDataHistory = new();
        
        /// <summary>
        /// A list of all the level data.
        /// </summary>
        public IReadOnlyList<LevelScoreData> LevelDataHistory => _levelDataHistory;
        
        /// <summary>
        /// Updates the level data.
        /// </summary>
        /// <param name="scoreData"> The score data of the level <see cref="LevelScoreData"/>. </param>
        internal void UpdateLevelData(LevelScoreData scoreData)
        {
            _levelDataHistory.Add(scoreData);
        }
        
        /// <summary>
        /// Gets the last added level data.
        /// </summary>
        /// <returns> The last added level data <see cref="LevelScoreData"/>. </returns>
        public LevelScoreData GetLatestLevelData()
        {
            // Get last element in the list
            return _levelDataHistory[^1];
        }
        
        /// <summary>
        /// Convenience method to get the highest score level data.
        /// </summary>
        /// <returns> The highest score level data <see cref="LevelScoreData"/>. </returns>
        public LevelScoreData GetHighestScoreLevelData()
        {
            return _levelDataHistory.Max();
        }
    }
}