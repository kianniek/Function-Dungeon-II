using System.Collections;
using System.Collections.Generic;
using TMPro;
using TurnSystem;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TurnDisplayText : MonoBehaviour
    {

        [Header("Variables")]
        [SerializeField] private string textFormat = "{0} {1}";
        [SerializeField] private TurnData turnData;

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
            _text.text = string.Format(textFormat, turnData.AmountOfTurns, turnData.CurrentTurn + 1);
        }
    }
}