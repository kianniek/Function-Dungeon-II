using UnityEngine;
using TMPro;
using System.Linq;
using LinearFunction;
using System.Collections.Generic;
using Extensions;
using UI;

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
            foreach (var button in inputButtons)
            {
                button.SetButtonValue(0, Color.white);
            }
            tableController.ResetYTexts();
        }

        private void InitializeInputTextComponents()
        {
            foreach (var button in inputButtons)
            {
                if (button == null)
                {
                    Debug.LogError("ExtendedButton is null during initialization.");
                    continue;
                }

                if (button.ButtonText == null)
                {
                    Debug.LogError($"ButtonText is null for button {button.name}.");
                    continue;
                }

                _inputDictionary.Add(button, button.ButtonText);
            }
        }

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

        private float GenerateRandomValue()
        {
            return Random.Range(linearFunctionData.MinSlope, linearFunctionData.MaxSlope).RoundValue(linearFunctionData.AmountOfDecimals);
        }

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

        private void AssignPreCalculatedValueToTable(int index)
        {
            var xValues = linearFunctionData.GetXValues.GetValues();
            if (xValues.Length > index)
            {
                var xValue = xValues[index];
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
