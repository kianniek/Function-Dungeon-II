using System.Collections.Generic;
using UnityEngine;

namespace Progression
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> that holds the progression data of the game.
    /// </summary>
    [CreateAssetMenu(fileName = "Game Progression Data", menuName = "Progression/Game Data")]
    public class GameProgressionData : ScriptableObject
    {
        private readonly Dictionary<string, LevelProgressionData> _gameProgression = new();
        
        /// <summary>
        /// Updates or adds the level score data to the game progression data.
        /// </summary>
        /// <param name="sceneName"> The name of the scene. </param>
        /// <param name="scoreData"> The score data of the level <see cref="LevelScoreData"/>. </param>
        internal void UpdateOrAddLevelScore(string sceneName, LevelScoreData scoreData)
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
        /// Tries to get the level data from the game progression data.
        /// </summary>
        /// <param name="sceneName"> The name of the scene. </param>
        /// <param name="levelProgressionData"> The level progression data of the level <see cref="LevelProgressionData"/>. </param>
        /// <returns> Returns true if the level data is found, otherwise false. </returns>
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