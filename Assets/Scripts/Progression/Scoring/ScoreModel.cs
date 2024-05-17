using Events.GameEvents.Typed;
using UnityEngine;

namespace Progression.Scoring
{
    /// <summary>
    /// Represents a score that can be added to the total score.
    /// </summary>
    public class ScoreModel : MonoBehaviour
    {
        [Header("Score Settings")]
        [SerializeField] private int scorePoints;
        
        [Header("Game Event")]
        [SerializeField] private IntGameEvent scoreAddEvent;
        
        /// <summary>
        /// The amount of points this script adds to the total score.
        /// </summary>
        public int ScorePoints => scorePoints;
        
        /// <summary>
        /// Adds the score points to the total score.
        /// </summary>
        public void AddScore()
        {
            scoreAddEvent?.Invoke(scorePoints);
        }
    }
}