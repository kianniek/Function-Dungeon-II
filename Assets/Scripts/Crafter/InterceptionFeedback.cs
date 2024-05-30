using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Crafter
{
    public class InterceptionFeedback : MonoBehaviour
    {
        [SerializeField] private InterceptionCalculator interceptionCalculator;
        [SerializeField] private TextMeshProUGUI xAnswer;
        [SerializeField] private TextMeshProUGUI yAnswer;

        [SerializeField] private UnityEvent onCorrectAnswerGivenEvent;
        [SerializeField] private UnityEvent onWrongAnswerGivenEvent;

        private Vector2 _correctInterceptionAnswer;

        /// <summary>
        /// Check if answer is correct when confirm button is clicked and fires unity event for wrong or right answer
        /// </summary>
        public void OnConfirmButtonClicked()
        {
            _correctInterceptionAnswer = interceptionCalculator.intersection;
            var answer = new Vector2(float.Parse(RemoveSpaces(xAnswer.text)), float.Parse(RemoveSpaces(yAnswer.text)));
            if (answer == _correctInterceptionAnswer)
            {
                onCorrectAnswerGivenEvent.Invoke();
            }
            else
            {
                onWrongAnswerGivenEvent.Invoke();
            }
        }

        /// <summary>
        /// Removes all spaces from a string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns></returns>
        private string RemoveSpaces(string input)
        {
            return Regex.Replace(input, @"[^0-9.,]+", "");
        }
    }
}

