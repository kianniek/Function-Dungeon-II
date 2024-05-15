using GameEvent.Events.Typed;
using UnityEngine;

namespace Progression.Scoring
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private int scorePoints;
        [SerializeField] private IntGameEvent scoreAddEvent;
        
        public int ScorePoints => scorePoints;
        
        public void AddScore()
        {
            scoreAddEvent.Invoke(scorePoints);
        }
    }
}