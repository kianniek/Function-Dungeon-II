using System.Collections.Generic;
using Events.GameEvents.Typed;
using Extensions;
using LinearFunction;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Table
{
    public class TableController : MonoBehaviour
    {
        private const string PlaceholderText = "N";
        
        private readonly Dictionary<Button, TMP_Text> _tableXDictionary = new();
        private readonly Dictionary<ExtendedButton, TMP_Text> _tableYDictionary = new();
        
        [Header("Data")]
        [SerializeField] private LinearFunctionData linearFunctionData;
        [SerializeField] private List<Button> tableXButtons;
        [SerializeField] private List<ExtendedButton> tableYButtons = new();

        [Header("Events")]
        [SerializeField] private FloatGameEvent onInputChanged;
        [SerializeField] private ExtendedButtonGameEvent onExtendedButtonClicked;
        [SerializeField] private ExtendedButton currentSelectedButton;

        [Header("Check Settings")]
        [SerializeField] private float checkMargin;
        [SerializeField] private UnityEvent onInputCorrect;
        [SerializeField] private UnityEvent onInputIncorrect;

        // GetColumnCount variable that returns the count of tableXButtons but can 't be set.
        public int GetColumnCount => tableXButtons.Count;
        
        private void OnEnable()
        {
            onInputChanged.AddListener(OnInputChanged);
            onExtendedButtonClicked.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            onInputChanged.RemoveListener(OnInputChanged);
            onExtendedButtonClicked.RemoveListener(OnButtonClicked);
        }

        private void Awake()
        {
            _tableXDictionary.Clear();
            _tableYDictionary.Clear();

            foreach (var button in tableXButtons)
            {
                _tableXDictionary[button] = button.GetComponentInChildren<TMP_Text>();
            }

            foreach (var extendedButton in tableYButtons)
            {
                _tableYDictionary[extendedButton] = extendedButton.GetComponentInChildren<TMP_Text>();
            }
        }

        private void Start()
        {
            if (tableXButtons.Count != tableYButtons.Count) 
                Debug.LogError("The two tables are not the same size");
        }

        public void OnButtonClicked(ExtendedButton extendedButton)
        {
            currentSelectedButton = extendedButton;
        }

        public void OnInputChanged(float value)
        {
            if (!currentSelectedButton)
                return;

            currentSelectedButton.ButtonValue = value;
            _tableYDictionary[currentSelectedButton].text = $"{value}";
        }

        public int GetXValue(int index)
        {
            return int.Parse(_tableXDictionary[tableXButtons[index]].text);
        }

        public void SetYValue(int index, float value)
        {
            var button = tableYButtons[index];
            
            _tableYDictionary[button].text = $"{value}";
            
            button.ButtonValue = value;
        }

        public void ResetYTexts()
        {
            foreach (var text in _tableYDictionary.Values)
            {
                text.text = PlaceholderText;
            }
        }

        public float[] GetYValues()
        {
            var values = new float[tableYButtons.Count];
            
            for (var i = 0; i < tableYButtons.Count; i++)
            {
                values[i] = tableYButtons[i].ButtonValue;
            }
            
            return values;
        }

        public float[] GetXValues()
        {
            var values = new float[tableXButtons.Count];
            
            for (var i = 0; i < tableXButtons.Count; i++)
            {
                values[i] = MathExtensions.RoundValue(
                    float.Parse(_tableXDictionary[tableXButtons[i]].text), 
                    linearFunctionData.AmountOfDecimals
                );
            }
            
            return values;
        }

        public void CheckInput()
        {
            var correct = true;
            var xValues = GetXValues();
            var yValues = GetYValues();

            for (var i = 0; i < tableXButtons.Count; i++)
            {
                var xValue = xValues[i];
                var yValue = yValues[i];

                var rawCorrectValue = LinearFunctionHelper.GetY(xValue, linearFunctionData.Slope, linearFunctionData.YIntercept);
                var correctValue = MathExtensions.RoundValue(rawCorrectValue, linearFunctionData.AmountOfDecimals);
                
                if (!(yValue < correctValue - checkMargin) && !(yValue > correctValue + checkMargin)) 
                    continue;
                
                correct = false;
                
                break;
            }

            if (correct)
            {
                Debug.Log("Correct");
                onInputCorrect?.Invoke();
            }
            else
            {
                Debug.Log("Incorrect");
                onInputIncorrect?.Invoke();
            }
        }
    }
}
