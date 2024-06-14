using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Crafter
{
    /// <summary>
    /// This class is responsible for checking if two lines intersect at a point.
    /// </summary>
    public class LineIntersectionChecker : MonoBehaviour
    {
        [Header("Correct Interception Variables")]
        [SerializeField] private FunctionLineController line1;
        [SerializeField] private FunctionLineController line2;

        [Header("GUI References")]
        [SerializeField] private TextMeshProUGUI xAnswer;
        [SerializeField] private TextMeshProUGUI yAnswer;

        [Header("Events")]
        [SerializeField] private UnityEvent onCorrectAnswerGivenEvent;
        [SerializeField] private UnityEvent onWrongAnswerGivenEvent;
        
        private float _a1;
        private float _b1;
        private float _a2;
        private float _b2;
        private Vector2 _intersection;

        private void Start()
        {
            _a1 = line1.A;
            _b1 = line1.transform.position.y;

            _a2 = line2.A;
            _b2 = line2.transform.position.y;

            _intersection = Vector2Extension.FindIntersection(_a1, _b1, _a2, _b2);
        }

        /// <summary>
        /// Check if answer is correct when confirm button is clicked and fires unity event for wrong or right answer
        /// </summary>
        public void OnConfirmButtonClicked()
        {
            var answer = new Vector2{
                x = float.Parse(StringExtensions.CleanUpDecimalOnlyString(xAnswer.text)),
                y = float.Parse(StringExtensions.CleanUpDecimalOnlyString(yAnswer.text))
            };

            if (answer == _intersection)
                onCorrectAnswerGivenEvent.Invoke();
            else
                onWrongAnswerGivenEvent.Invoke();
        }
    }
}

