using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private string scoreTextFormat = "Score: {0}";
        
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }
        
        public void SetScore(int score)
        {
            _text.text = string.Format(scoreTextFormat, score);
        }
    }
}