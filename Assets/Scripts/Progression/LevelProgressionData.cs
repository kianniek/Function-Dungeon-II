using System;
using System.Collections.Generic;

namespace Progression
{
    [Serializable]
    public class LevelProgressionData
    {
        private readonly List<LevelScoreData> _levelDataHistory = new();
        
        public IReadOnlyList<LevelScoreData> LevelDataHistory => _levelDataHistory;
        
        public void UpdateLevelData(LevelScoreData scoreData)
        {
            _levelDataHistory.Add(scoreData);
        }
        
        public LevelScoreData GetLatestLevelData()
        {
            // Get last element in the list
            return _levelDataHistory[^1];
        }
    }
}