using UnityEngine;
using TMPro;
using System.Linq;
using LinearFunction;
using System.Collections.Generic;
using Extensions;
using UI;
using Utils;

namespace Table
{
    public class InputButtonController : MonoBehaviour
    {
        private readonly Dictionary<ExtendedButton, TMP_Text> _inputDictionary = new();
        [SerializeField] private LinearFunctionData linearFunctionData;
        [SerializeField] private List<ExtendedButton> inputButtons;
        [SerializeField] private TableController tableController;

        private int _tableColumnCount;

        private void Start()
        {
            ShuffleInputButtons();
            InitializeInputTextComponents();
            _tableColumnCount = tableController.ColumnCount;
            ChangeInputNumbers();
        }

        /// <summary>
        /// Changes the input numbers on the buttons and the table.
        /// </summary>
        public void ChangeInputNumbers()
        {
            if (!ValidateAmounts())
                return;

            AssignRandomValuesToButtons();

            var preCalculatedIndices = new HashSet<int>(
                TableHelper.GenerateRandomIndices(tableController.AmountGivenValues, _tableColumnCount));

            for (var i = 0; i < _tableColumnCount; i++)
            {
                if (!preCalculatedIndices.Contains(i))
                {
                    AssignCalculatedValueToButton(i, linearFunctionData.CorrectTableValues.ElementAt(i).Value);
                }
            }

            // Assign pre-calculated values to the table
            var index = 0;
            foreach (var preCalculatedIndex in preCalculatedIndices)
            {
                AssignPreCalculatedValueToTable(preCalculatedIndex);
                index++;
            }
        }

        /// <summary>
        /// Resets the input buttons to their default state.
        /// </summary>
        public void ResetInputButtons()
        {
            // Reset each button's value and color
            foreach (var button in inputButtons)
            {
                button.SetButtonValue(0, Color.white);
            }
            
            // Reset the table controller's Y texts
            tableController.ResetYTexts();
        }
        
        /// <summary>
        /// Initializes the TMP_Text components for each input button.
        /// </summary>
        private void InitializeInputTextComponents()
        {
            foreach (var button in inputButtons)
            {
                _inputDictionary.Add(button, button.ButtonText);
            }
        }
        
        /// <summary>
        /// Validates if the amount of given values is appropriate.
        /// </summary>
        /// <returns>True if valid, otherwise false.</returns>
        private bool ValidateAmounts()
        {
            if (tableController.AmountGivenValues > _tableColumnCount)
            {
                Debug.LogError("The amount of given values is higher than the amount of columns in the table");
                return false;
            }
            
            if (inputButtons.Count >= linearFunctionData.AmountOfDecimals)
                return true;
            
            Debug.LogError("The amount of input buttons is lower than the amount of given values");
            return false;
        }

        private void AssignRandomValuesToButtons()
        {
            foreach (var button in inputButtons)
            {
                var value = GenerateRandomValue();
                if (_inputDictionary.TryGetValue(button, out var inputText))
                {
                    button.SetButtonValue(value);
                }
                else
                {
                    Debug.LogWarning($"Button {button.name} not found in dictionary during random value assignment.");
                }
            }
        }
        
        /// <summary>
        /// Generates a random value within the specified range.
        /// </summary>
        /// <returns>A rounded random value.</returns>
        private float GenerateRandomValue()
        {
            return Random.Range(linearFunctionData.MinSlope, linearFunctionData.MaxSlope).RoundValue(linearFunctionData.AmountOfDecimals);
        }
        
        /// <summary>
        /// Assigns a calculated value to the specified button.
        /// </summary>
        /// <param name="index">Index of the button to assign the value to.</param>
        /// <param name="value">the value to assign to the button.</param>
        private void AssignCalculatedValueToButton(int index, float value)
        {
            var button = inputButtons[index];

            if (!_inputDictionary.TryGetValue(button, out var inputText))
            {
                Debug.LogError($"Button at index {index} not found in dictionary.");
                return;
            }
#if UNITY_EDITOR
            button.SetButtonValue(value, Color.cyan);
#else
            button.SetButtonValue(value);
#endif
        }
        
        /// <summary>
        /// Assigns a pre-calculated value to the table.
        /// </summary>
        /// <param name="index">Index of the table column to assign the value to.</param>
        private void AssignPreCalculatedValueToTable(int index)
        {
            var xValues = linearFunctionData.GetXValues;
            if (xValues.Length > index)
            {
                var xValue = xValues.Values[index];
                if (linearFunctionData.CorrectTableValues.TryGetValue(xValue, out var value))
                {
                    tableController.SetYButtonValue(index, value);
                }
                else
                {
                    Debug.LogWarning($"Correct table value for xValue {xValue} not found.");
                }
            }
            else
            {
                Debug.LogWarning($"xValues array length is less than index {index}.");
            }
        }
        
        /// <summary>
        /// Shuffles the input buttons randomly.
        /// </summary>
        private void ShuffleInputButtons()
        {
            var n = inputButtons.Count;
            while (n > 1)
            {
                n--;
                var k = Random.Range(0, n + 1);
                (inputButtons[k], inputButtons[n]) = (inputButtons[n], inputButtons[k]);
            }
        }
    }
}