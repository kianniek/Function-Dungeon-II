using UnityEngine;
using TMPro;
using System.Linq;
using LinearFunction;
using System.Collections.Generic;
using Extensions;
using UI;

namespace Table
{
    /// <summary>
    /// Manages the behavior and interaction of input buttons in the table UI.
    /// </summary>
    public class InputButtonController : MonoBehaviour
    {
        // Dictionary to map ExtendedButton to their corresponding TMP_Text component
        private readonly Dictionary<ExtendedButton, TMP_Text> _inputDictionary = new();
        
        // Serialized fields to be assigned via Unity Inspector
        [SerializeField] private LinearFunctionData linearFunctionData;
        [SerializeField] private List<ExtendedButton> inputButtons;
        [SerializeField] private TableController tableController;
        [SerializeField] private float valueRange;
        [SerializeField] private int amountOfGivenValues = 2;
        
        // Count of columns in the table
        private int _tableColumnCount;
        
        private void Awake()
        {
            // Shuffle the input buttons and initialize their text components
            ShuffleInputButtons();
            InitializeInputTextComponents();
            
            // Set the table column count from the linear function data
            _tableColumnCount = tableController.ColumnCount;
            
            // Change the input numbers based on the linear function data
            ChangeInputNumbers();
        }
        
        /// <summary>
        /// Changes the input numbers based on the linear function data.
        /// </summary>
        public void ChangeInputNumbers()
        {
            // Validate the amounts before proceeding
            if (!ValidateAmounts())
                return;
            
            // Assign random values to the buttons
            AssignRandomValuesToButtons();
            
            // Generate random indices for pre-calculated values
            var preCalculatedIndices = new HashSet<int>(
                TableHelper.GenerateRandomIndices(amountOfGivenValues, _tableColumnCount));
            
            // Assign calculated values to buttons that are not pre-calculated
            for (var i = 0; i < _tableColumnCount; i++)
            {
                if (!preCalculatedIndices.Contains(i))
                {
                    AssignCalculatedValueToButton(i, linearFunctionData.CorrectTableValues.ElementAt(i).Value);
                }
            }
            
            // Assign pre-calculated values to the table
            int index = 0;
            
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
                button.SetButtonValue(button.GetComponentInChildren<TMP_Text>(), 0, Color.white);
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
                _inputDictionary.Add(button, button.GetComponentInChildren<TMP_Text>());
            }
        }
        
        /// <summary>
        /// Validates if the amount of given values is appropriate.
        /// </summary>
        /// <returns>True if valid, otherwise false.</returns>
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
        
        /// <summary>
        /// Assigns random values to each input button.
        /// </summary>
        private void AssignRandomValuesToButtons()
        {
            foreach (var button in inputButtons)
            {
                var value = GenerateRandomValue();
                
                if (_inputDictionary.TryGetValue(button, out var text))
                    button.SetButtonValue(text, value);
            }
        }
        
        /// <summary>
        /// Generates a random value within the specified range.
        /// </summary>
        /// <returns>A rounded random value.</returns>
        private float GenerateRandomValue()
        {
            // Round the value to the amount of decimals specified in the linear function data
            return MathfExtentions.RoundValue(
                Random.Range(-valueRange, valueRange),
                linearFunctionData.AmountOfDecimals
            );
        }
        
        /// <summary>
        /// Assigns a calculated value to the specified button.
        /// </summary>
        /// <param name="index">Index of the button to assign the value to.</param>
        /// <param name="value">the value to assing to the button.</param>
        private void AssignCalculatedValueToButton(int index, float value)
        {
            if (_inputDictionary.TryGetValue(inputButtons[index], out var inputText))
                inputButtons[index].SetButtonValue(inputText, value, Color.cyan);
        }
        
        /// <summary>
        /// Assigns a pre-calculated value to the table.
        /// </summary>
        /// <param name="index">Index of the table column to assign the value to.</param>
        private void AssignPreCalculatedValueToTable(int index)
        {
            var xValues = linearFunctionData.GetXValues.GetValues();
            var xValue = xValues[index];
            var value = linearFunctionData.CorrectTableValues[xValue];
            
            tableController.SetYButtonValue(index, value);
        }
        
        /// <summary>
        /// Shuffles the input buttons randomly.
        /// </summary>
        private void ShuffleInputButtons()
        {
            inputButtons = inputButtons.OrderBy(x => Random.Range(0, inputButtons.Count)).ToList();
        }
    }
}