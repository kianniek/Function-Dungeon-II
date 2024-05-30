using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Crafter
{
    public class InterceptionFeedback : MonoBehaviour
    {
        [Header("Correct Interception Reference")]
        [SerializeField] private InterceptionCalculator interceptionCalculator;

        [Header("GUI References")]
        [SerializeField] private TextMeshProUGUI xAnswer;
        [SerializeField] private TextMeshProUGUI yAnswer;

        [Header("Events")]
        [SerializeField] private UnityEvent onCorrectAnswerGivenEvent;
        [SerializeField] private UnityEvent onWrongAnswerGivenEvent;
        
        /// <summary>
        /// Check if answer is correct when confirm button is clicked and fires unity event for wrong or right answer
        /// </summary>
        public void OnConfirmButtonClicked()
        {
            var answer = new Vector2(
                float.Parse(StringManipulation.CleanUpDecimalOnlyString(xAnswer.text)), 
                float.Parse(StringManipulation.CleanUpDecimalOnlyString(yAnswer.text))
            );
            
            if (answer == interceptionCalculator.Intersection)
                onCorrectAnswerGivenEvent.Invoke();
            else
                onWrongAnswerGivenEvent.Invoke();
        }
    }
}

