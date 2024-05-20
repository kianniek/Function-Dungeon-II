using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class ResourceText : MonoBehaviour
    {
        [SerializeField] private string scoreTextFormat = "Resource: {0}";
        
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }
        
        public void SetScore(int value)
        {
            _text.text = string.Format(scoreTextFormat, value);
        }
    }
}