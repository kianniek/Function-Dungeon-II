using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
using LinearFunction;
using ButtonExtended;
using Table;
using System.Collections.Generic;
using static UnityEngine.EventSystems.PointerEventData;

namespace Table
{
    public class InputButtonController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private LinearFunctionData linearFunctionData;
        [SerializeField] private List<ExtendedButton> inputButtons;
        [SerializeField] private TableController tableController;
        [SerializeField] private float valueRange;
        [SerializeField] private int amountOfGivenValues = 2;

        [Header("Debug")]
        [SerializeField] bool debugMode;
        
        private Dictionary<ExtendedButton, TMP_Text> _inputDictionary = new();
        private int _tableColumnCount;

        /// <summary>
        /// Changes the input numbers based on the linear function data.
        /// </summary>
        public void ChangeInputNumbers()
        {
            if (!ValidateAmounts()) 
                return;

            AssignRandomValuesToButtons();

            var preCalculatedIndices = TableHelper.GenerateRandomIndices(amountOfGivenValues, _tableColumnCount);
            preCalculatedIndices = preCalculatedIndices.OrderBy(x => x).ToList();
            foreach (var preCalculatedIndex in preCalculatedIndices)
            {
                Debug.Log("Pre-calculated index: " + preCalculatedIndex);
            }
            for (var i = 0; i < _tableColumnCount; i++)
            {
                if (preCalculatedIndices.Contains(i)) continue;
                AssignCalculatedValueToButton(i);
            }

            for (var i = 0; i < amountOfGivenValues; i++)
            {
                AssignPreCalculatedValueToTable(preCalculatedIndices.ToArray()[i]);
            }
        }

        /// <summary>
        /// Resets the input buttons to their default state.
        /// </summary>
        public void ResetInputButtons()
        {
            foreach (var button in inputButtons)
            {
                SetButtonValue(button, button.GetComponentInChildren<TMP_Text>(), 0, Color.white);
            }

            tableController.ResetYTexts();
        }

        private void Awake()
        {
            ShuffleInputButtons();
            InitializeInputTextComponents();
            _tableColumnCount = tableController.GetColumnCount;
        }

        private void InitializeInputTextComponents()
        {
            for (var i = 0; i < inputButtons.Count; i++)
            {
                var button = inputButtons[i];
                _inputDictionary.Add(button, button.GetComponentInChildren<TMP_Text>());
            }
        }

        private bool ValidateAmounts()
        {
            if (amountOfGivenValues > _tableColumnCount)
            {
                Debug.LogError("The amount of given values is higher than the amount of columns in the table");
                return false;
            }

            if (inputButtons.Count < amountOfGivenValues)
            {
                Debug.LogError("The amount of input buttons is lower than the amount of given values");
                return false;
            }
            return true;
        }

        private void AssignRandomValuesToButtons()
        {
            foreach (var t in inputButtons)
            {
                var value = GenerateRandomValue();
                
                if(_inputDictionary.TryGetValue(t, out var text))
                {
                    SetButtonValue(t, text, value);
                }
            }
        }

        private float GenerateRandomValue()
        {
            var value = UnityEngine.Random.Range(-valueRange, valueRange);
            // Round the value to the amount of decimals specified in the linear function data.
            return MathfExtentions.RoundValue(value, linearFunctionData.AmountOfDecimals);
        }

        private void AssignCalculatedValueToButton(int index)
        {
            var xValue = tableController.GetXValue(index);
            var value = LinearFunctionHelper.GetY(xValue, linearFunctionData.Slope, linearFunctionData.YIntercept);
            value = MathfExtentions.RoundValue(value, linearFunctionData.AmountOfDecimals);

            if (_inputDictionary.TryGetValue(inputButtons[index], out var inputText))
            {
                SetButtonValue(inputButtons[index], inputText, value, Color.cyan, debugMode);
                Debug.Log(value);
            }
        }

        private void AssignPreCalculatedValueToTable(int index)
        {
            var xValue = tableController.GetXValue(index);
            var value = LinearFunctionHelper.GetY(xValue, linearFunctionData.Slope, linearFunctionData.YIntercept);
            tableController.SetYValue(index, value);
        }

        private void SetButtonValue(ExtendedButton button, TMP_Text text, float value)
        {
            SetButtonValue(button, text, value, Color.clear, false);
        }

        private void SetButtonValue(ExtendedButton button, TMP_Text text, float value, Color color)
        {
            SetButtonValue(button, text, value, color, true);
        }

        private void SetButtonValue(ExtendedButton button, TMP_Text text, float value, Color color, bool useColor)
        {
            button.ButtonValue = value;
            text.text = value.ToString();
            if (useColor)
            {
                button.Button.colors = GetColorBlock(color);
            }
        }

        private void ShuffleInputButtons()
        {
            inputButtons = inputButtons.OrderBy(x => UnityEngine.Random.Range(0, inputButtons.Count)).ToList();
        }

        private static ColorBlock GetColorBlock(Color color)
        {
            return new ColorBlock
            {
                normalColor = color,
                highlightedColor = color,
                pressedColor = color,
                selectedColor = color,
                disabledColor = color,
                colorMultiplier = 1,
                fadeDuration = 0.1f
            };
        }
    }
}