using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
using LinearFunction;
using ButtonExtended;
using Table;

namespace Tabel
{
    public class InputButtonController : MonoBehaviour
    {
        [SerializeField] private LinearFunctionData linearFunctionData;
        [SerializeField] private ExtendedButton[] inputButtons;
        [SerializeField] private TabelController tabelController;
        [SerializeField] private float valueRange;
        [SerializeField] private int amountOfGivenValues = 2;

        private TMP_Text[] _inputText;
        private int _tableColumnCount;

        /// <summary>
        /// Changes the input numbers based on the linear function data.
        /// </summary>
        public void ChangeInputNumbers()
        {
            if (!ValidateAmounts()) return;

            AssignRandomValuesToButtons();

            var preCalculatedIndices = GenerateRandomIndices(amountOfGivenValues, _tableColumnCount);

            for (var i = 0; i < _tableColumnCount; i++)
            {
                if (!preCalculatedIndices.Contains(i))
                {
                    AssignCalculatedValueToButton(i);
                }
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
                SetButtonValue(button, button.GetComponentInChildren<TMP_Text>(), 0, Color.white);
            }

            tabelController.ResetYTexts();
        }

        private void Awake()
        {
            ShuffleInputButtons();
            InitializeInputTextComponents();
            _tableColumnCount = tabelController.GetColumnCount();
        }

        private void InitializeInputTextComponents()
        {
            _inputText = new TMP_Text[inputButtons.Length];
            for (var i = 0; i < inputButtons.Length; i++)
            {
                _inputText[i] = inputButtons[i].GetComponentInChildren<TMP_Text>();
            }
        }

        private bool ValidateAmounts()
        {
            if (amountOfGivenValues > _tableColumnCount)
            {
                Debug.LogError("The amount of given values is higher than the amount of columns in the table");
                return false;
            }

            if (inputButtons.Length < amountOfGivenValues)
            {
                Debug.LogError("The amount of input buttons is lower than the amount of given values");
                return false;
            }
            return true;
        }

        private void AssignRandomValuesToButtons()
        {
            for (var i = 0; i < inputButtons.Length; i++)
            {
                var value = GenerateRandomValue();
                SetButtonValue(inputButtons[i], _inputText[i], value);
            }
        }

        private float GenerateRandomValue()
        {
            var value = UnityEngine.Random.Range(-valueRange, valueRange);
            return Mathf.Round(value * Mathf.Pow(10, linearFunctionData.AmountOfDecimals)) / Mathf.Pow(10, linearFunctionData.AmountOfDecimals);
        }

        private void AssignCalculatedValueToButton(int index)
        {
            var xValue = tabelController.GetXValue(index);
            var value = LinearFunctionHelper.GetY(xValue, linearFunctionData.Slope, linearFunctionData.YIntercept);
            value = Mathf.Round(value * Mathf.Pow(10, linearFunctionData.AmountOfDecimals)) / Mathf.Pow(10, linearFunctionData.AmountOfDecimals);

            SetButtonValue(inputButtons[index], _inputText[index], value, Color.cyan);
        }

        private void AssignPreCalculatedValueToTable(int index)
        {
            var xValue = tabelController.GetXValue(index);
            var value = LinearFunctionHelper.GetY(xValue, linearFunctionData.Slope, linearFunctionData.YIntercept);
            tabelController.SetYValue(index, value);
        }

        private void SetButtonValue(ExtendedButton button, TMP_Text text, float value, Color? color = null)
        {
            button.ButtonValue = value;
            text.text = value.ToString();
            if (color.HasValue)
            {
                button.colors = GetColorBlock(color.Value);
            }
        }

        

        private int[] GenerateRandomIndices(int count, int maxIndex)
        {
            var indices = Enumerable.Range(0, maxIndex).OrderBy(x => UnityEngine.Random.Range(0, maxIndex)).Take(count).ToArray();
            return indices;
        }

        private void ShuffleInputButtons()
        {
            inputButtons = inputButtons.OrderBy(x => UnityEngine.Random.Range(0, inputButtons.Length)).ToArray();
        }

        private ColorBlock GetColorBlock(Color color)
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