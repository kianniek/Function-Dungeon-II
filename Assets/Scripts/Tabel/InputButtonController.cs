using UnityEngine;
using TMPro;
using LinearFunction;
using ButtonExtended;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class InputButtonController : MonoBehaviour
{
    [SerializeField] private ExtendedButton[] inputButtons;
    [SerializeField] private TabelController tabelController;
    [SerializeField] private float valueRange;
    [SerializeField] private int amountBehindComma = 1;
    [SerializeField] private int amountOfGivenValues = 2;

    private TMP_Text[] _inputText;
    private int _tableColumnCount;
    private float _slope;
    private float _yIntercept;

    private void Awake()
    {
        ShuffleInputButtons();
        _inputText = new TMP_Text[inputButtons.Length];
        for (var i = 0; i < inputButtons.Length; i++)
        {
            _inputText[i] = inputButtons[i].GetComponentInChildren<TMP_Text>();
        }
        _tableColumnCount = tabelController.GetColumnCount();
    }

    public void SetSlope(float slope)
    {
        _slope = slope;
    }

    public void SetYIntercept(float yIntercept)
    {
        _yIntercept = yIntercept;
    }

    public void ChangeInputNumbers()
    {
        if (amountOfGivenValues > _tableColumnCount)
        {
            Debug.LogError("The amount of given values is higher than the amount of columns in the table");
            return;
        }

        if (inputButtons.Length < amountOfGivenValues)
        {
            Debug.LogError("The amount of input buttons is lower than the amount of given values");
            return;
        }
        for (var i = 0; i < inputButtons.Length; i++)
        {
            var value = UnityEngine.Random.Range(-valueRange, valueRange);
            value = MathF.Round(value, amountBehindComma);
            inputButtons[i].ButtonValue = value;
            _inputText[i].text = value.ToString();
        }

        // Generate random indices for the pre-calculated values to be shown in the table
        var preCalculatedIndices = GenerateRandomIndices(amountOfGivenValues, _tableColumnCount);
        Debug.Log("Random indices: " + string.Join(", ", preCalculatedIndices));

        for (var i = 0; i < _tableColumnCount; i++)
        {
            if (preCalculatedIndices.Contains(i))
                continue;

            var xValue = tabelController.GetXValue(i);
            var value = LinearFunction.LinearFunction.GetY(xValue, _slope, _yIntercept);
            value = MathF.Round(value, amountBehindComma);

            ColorBlock colorBlock = new ColorBlock();
            colorBlock.normalColor = Color.cyan;
            colorBlock.highlightedColor = Color.cyan;
            colorBlock.pressedColor = Color.cyan;
            colorBlock.selectedColor = Color.cyan;
            colorBlock.disabledColor = Color.cyan;
            colorBlock.colorMultiplier = 1;
            colorBlock.fadeDuration = 0.1f;

            //debug for xValue and value
            Debug.Log("xValue: " + xValue + " value: " + value);
            inputButtons[i].colors = colorBlock;
            inputButtons[i].ButtonValue = value;
            _inputText[i].text = value.ToString();
            Debug.Log("Value text: " + _inputText[i].text);
        }


        for (var i = 0; i < amountOfGivenValues; i++)
        {
            var xValue = tabelController.GetXValue(preCalculatedIndices[i]);
            var value = LinearFunction.LinearFunction.GetY(xValue, _slope, _yIntercept);
            Debug.Log("xValue: " + xValue + " value: " + value);
            tabelController.SetYValue(preCalculatedIndices[i], value);
        }
    }

    public void ResetInputButtons()
    {
        // Reset each input button to default values and color
        foreach (var button in inputButtons)
        {
            button.ButtonValue = 0;
            var textComponent = button.GetComponentInChildren<TMP_Text>();
            if (textComponent != null)
            {
                textComponent.text = "X";
            }

            ColorBlock colorBlock = new ColorBlock();
            colorBlock.normalColor = Color.white;
            colorBlock.highlightedColor = Color.white;
            colorBlock.pressedColor = Color.white;
            colorBlock.selectedColor = Color.white;
            colorBlock.disabledColor = Color.white;
            colorBlock.colorMultiplier = 1;
            colorBlock.fadeDuration = 0.1f;

            button.colors = colorBlock;
        }

        // Reset table values to default
        tabelController.ResetYTexts();
    }

    private int[] GenerateRandomIndices(int count, int maxIndex)
    {
        var indices = new List<int>();
        for (var i = 0; i < maxIndex; i++)
        {
            indices.Add(i);
        }

        // Shuffle the indices array
        for (var i = 0; i < indices.Count; i++)
        {
            var temp = indices[i];
            var randomIndex = UnityEngine.Random.Range(i, indices.Count);
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        // Take 'count' number of random indices
        var randomIndices = indices.Take(count).ToArray();
        return randomIndices;
    }

    private void ShuffleInputButtons()
    {
        for (var i = 0; i < inputButtons.Length; i++)
        {
            var temp = inputButtons[i];
            var randomIndex = UnityEngine.Random.Range(i, inputButtons.Length);
            inputButtons[i] = inputButtons[randomIndex];
            inputButtons[randomIndex] = temp;
        }
    }
}
