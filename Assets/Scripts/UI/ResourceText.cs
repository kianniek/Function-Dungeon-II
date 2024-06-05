using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class ResourceText : MonoBehaviour
    {
        [SerializeField] private string textFormat = "Resource: {0}";
        
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }
        
        /// <summary>
        /// Set the value of the text
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(int value)
        {
            _text.text = string.Format(textFormat, value);
        }
        
        /// <summary>
        /// Set the value of the text
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(string value)
        {
            _text.text = string.Format(textFormat, value);
        }
        
        /// <summary>
        /// Set the value of the text
        /// </summary>
        /// <param name="value"></param>
        /// <param name="value1"></param>
        /// <param name="newLine">Put a new line symbol at the end of the</param>
        public void SetValue(string value, string value1, bool newLine = false)
        {
            if (newLine)
            {
                _text.text += "\n";
            }
            _text.text = string.Format(textFormat, value, value1);

        }
    }
}