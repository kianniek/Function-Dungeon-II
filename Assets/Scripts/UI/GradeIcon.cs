using Progression.Grading;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class GradeIcon : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void SetGrade(Grade grade)
        {
            _text.text = grade.GradeName;
        }
    }
}