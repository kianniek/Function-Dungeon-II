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
        [SerializeField] private float valueRange;
        [SerializeField] private int amountOfGivenValues = 2;
        
        private int _tableColumnCount;
        
        /// <summary>
        /// Changes the input numbers based on the linear function data.
        /// </summary>
        public void ChangeInputNumbers()
        {
            if (!ValidateAmounts())
                return;
            
            AssignRandomValuesToButtons();
            
            var preCalculatedIndices = TableHelper
                .GenerateRandomIndices(amountOfGivenValues, _tableColumnCount)
                .ToArray();
            
            for (var i = 0; i < _tableColumnCount; i++)
            {
                if (!preCalculatedIndices.Contains(i))
                    AssignCalculatedValueToButton(i);
            }
            
            for (var i = 0; i < amountOfGivenValues; i++)
            {
                AssignPreCalculatedValueToTable(preCalculatedIndices[i]);
            }
        }
        
        /// <summary>
        /// Resets the input buttons to their default state.
        /// </summary>
        public void ResetInputButtons()
        {
            foreach (var button in inputButtons)
            {
                button.SetButtonValue(button.GetComponentInChildren<TMP_Text>(), 0, Color.white);
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
            foreach (var button in inputButtons)
            {
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
            foreach (var button in inputButtons)
            {
                var value = GenerateRandomValue();
                
                if (_inputDictionary.TryGetValue(button, out var text))
                    button.SetButtonValue(text, value);
            }
        }
        
        private float GenerateRandomValue()
        {
            // Round the value to the amount of decimals specified in the linear function data.
            return MathfExtentions.RoundValue(
                Random.Range(-valueRange, valueRange),
                linearFunctionData.AmountOfDecimals
            );
        }
        
        private void AssignCalculatedValueToButton(int index)
        {
            var xValue = tableController.GetXValue(index);
            var yValue = LinearFunctionHelper.GetY(xValue, linearFunctionData.Slope, linearFunctionData.YIntercept);
            
            yValue = MathfExtentions.RoundValue(yValue, linearFunctionData.AmountOfDecimals);
            
            if (_inputDictionary.TryGetValue(inputButtons[index], out var inputText))
                inputButtons[index].SetButtonValue(inputText, yValue, Color.cyan);
        }
        
        private void AssignPreCalculatedValueToTable(int index)
        {
            var xValue = tableController.GetXValue(index);
            var value = LinearFunctionHelper.GetY(xValue, linearFunctionData.Slope, linearFunctionData.YIntercept);
            
            tableController.SetYValue(index, value);
        }
        
        private void ShuffleInputButtons()
        {
            inputButtons = inputButtons.OrderBy(x => Random.Range(0, inputButtons.Count)).ToList();
        }
    }
}