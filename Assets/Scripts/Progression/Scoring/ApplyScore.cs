using Events.GameEvents.Typed;
using UnityEngine;

namespace Progression.Scoring
{
    /// <summary>
    /// Represents a score that can be added to the total score.
    /// </summary>
    public class ApplyScore : MonoBehaviour
    {
        [Header("Score Settings")]
        [SerializeField] private int scorePoints;
        
        [Header("Game Events")] 
        [SerializeField] private IntGameEvent onApplyScore;
        
        /// <summary>
        /// The amount of points this script adds to the total score.
        /// </summary>
        public int ScorePoints => scorePoints;
        
        /// <summary>
        /// Adds the score points to the total score.
        /// </summary>
        public void AddScore()
        {
            onApplyScore?.Invoke(scorePoints);
        }
    }
}