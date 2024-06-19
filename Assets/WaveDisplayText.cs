using Kaijus;
using TMPro;
using TurnSystem;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class WaveDisplayText : MonoBehaviour
    {

        [Header("Variables")]
        [SerializeField] private string textFormat = "{0}";
        [SerializeField] private KaijuLevel KaijuLevel;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        /// <summary>
        /// Updates the text based on the given data
        /// </summary>
        public void UpdateText()
        {
            _text.text = string.Format(textFormat, KaijuLevel.CurrentKaijuInLevel + 1);
        }
    }
}