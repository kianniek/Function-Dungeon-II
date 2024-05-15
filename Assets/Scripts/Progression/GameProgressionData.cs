using System;
using System.Collections.Generic;
using UnityEngine;

namespace Progression
{
    [CreateAssetMenu(fileName = "Game Progression Data", menuName = "Game Data/Progression Container")]
    public class GameProgressionData : ScriptableObject
    {
        private readonly Dictionary<string, LevelProgressionData> _gameProgression = new();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="scoreData"></param>
        public void UpdateOrAddLevelScore(string sceneName, LevelScoreData scoreData)
        {
            if (_gameProgression.TryGetValue(sceneName, out var levelProgressionData))
            {
                levelProgressionData.UpdateLevelData(scoreData);
            }
            else
            {
                _gameProgression.Add(sceneName, new LevelProgressionData());
                _gameProgression[sceneName].UpdateLevelData(scoreData);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="levelProgressionData"></param>
        /// <returns></returns>
        public bool TryGetLevelData(string sceneName, out LevelProgressionData levelProgressionData)
        {
            return _gameProgression.TryGetValue(sceneName, out levelProgressionData);
        }
        
        private void Reset()
        {
            _gameProgression.Clear();
        }
    }
}