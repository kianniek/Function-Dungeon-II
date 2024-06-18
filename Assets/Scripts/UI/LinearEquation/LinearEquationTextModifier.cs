using TMPro;
using UnityEngine;

namespace UI.LinearEquation
{
    /// <summary>
    /// This script is responsible for changing the text of a TMP_Text component to represent a linear equation
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class LinearEquationTextModifier : MonoBehaviour
    {
        private const string EquationFormat = "y = {0}x + {1}";
        private const string EquationFormatNoHeight = "y = {0}x";
        
        [Header("Variables")]
        [SerializeField] private float startAVariable;
        [SerializeField] private float startBVariable;

        [Header("Visual Settings")] 
        [SerializeField] private bool showHeight = true;
        
        private TMP_Text _text;
        private float _aVariable;
        private float _bVariable;
        
        /// <summary>
        /// Change the A, aka slope or steepness, variable of the equation
        /// </summary>
        public float AVariable
        {
            get => _aVariable;
            set
            {
                _aVariable = value;
                
                UpdateText();
            }
        }

        /// <summary>
        /// Change the B, aka y-intercept or height, variable of the equation
        /// </summary>
        public float BVariable
        {
            get => _bVariable;
            set
            {
                _bVariable = value;
                
                UpdateText();
            }
        }

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }
        
        private void Start()
        {
            AVariable = startAVariable;
            BVariable = startBVariable;
        }

        private void UpdateText()
        {
            _text.text = showHeight ? 
                string.Format(EquationFormat, _aVariable, _bVariable) : 
                string.Format(EquationFormatNoHeight, _aVariable);
        }
    }
}