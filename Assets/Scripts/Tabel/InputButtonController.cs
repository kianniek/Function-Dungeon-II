using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LinearFunction;
using ButtonExtended;
using TMPro;
using System;

public class InputButtonController : MonoBehaviour
{
    [SerializeField] private ExtendedButton[] inputButtons;
    [SerializeField] private TabelController tabelController;
    [Tooltip("The random range in both negative and positive values the non awnser buttons will have")]
    [SerializeField]  private float valueRange;
    [SerializeField] private int amountBehindComma = 1;
    [SerializeField] private int amountOfGivenValues = 2;
    [SerializeField] private int[] givenYindex;

    private int tableCollumCount;
    private TMP_Text[] _inputText;
    private float _slope;
    private float _yIntersept;

    

    private void Awake()
    {
        //randomly sort the input buttons
        for (var i = 0; i < inputButtons.Length; i++)
        {
            var temp = inputButtons[i];
            var randomIndex = UnityEngine.Random.Range(i, inputButtons.Length);
            inputButtons[i] = inputButtons[randomIndex];
            inputButtons[randomIndex] = temp;
        }

        _inputText = new TMP_Text[inputButtons.Length];
        for (var i = 0; i < inputButtons.Length; i++)
        {
            _inputText[i] = inputButtons[i].GetComponentInChildren<TMP_Text>();
        }

        tableCollumCount = tabelController.GetCollumCount();

        givenYindex = new int[amountOfGivenValues];
    }

    public void SetSlope(float slope)
    {
        _slope = slope;
        Debug.Log("Slope: " + _slope);
        ChangeInputNumbers();
    }

    public void SetYIntersept(float yIntersept)
    {
        _yIntersept = yIntersept;
        Debug.Log("YIntersept: " + yIntersept);
        ChangeInputNumbers();
    }

    public void ChangeInputNumbers()
    {
        if(givenYindex.Length > tableCollumCount)
        {
            Debug.LogError("The amount of given values is higher than the amount of collums in the tabel");
            return;
        }

        if(inputButtons.Length < amountOfGivenValues)
        {
            Debug.LogError("The amount of input buttons is lower than the amount of given values");
            return;
        }

        var amountPreCalculatedValues = tableCollumCount - amountOfGivenValues;

        //set the first values to the precalculated values
        for (var i = 0; i < amountPreCalculatedValues; i++)
        {
            var value = LinearFunction.LinearFunction.GetY(tabelController.GetXValue(i), _slope, _yIntersept);
            value = MathF.Round(value, amountBehindComma);

            Debug.Log($"For Slope:{_slope} and Y-intersept:{_yIntersept} value input added:{value}");

            inputButtons[i].ButtonValue = value;
            _inputText[i].text = value.ToString();
        }

        for (var i = amountPreCalculatedValues; i < inputButtons.Length; i++)
        {
            var value = UnityEngine.Random.Range(-valueRange, valueRange);
            value = MathF.Round(value, amountBehindComma);

            inputButtons[i].ButtonValue = value;
            _inputText[i].text = value.ToString();
        }

        for (var i = 0; i < amountOfGivenValues; i++)
        {
            //var randomIndex = UnityEngine.Random.Range(0, tableCollumCount);

            ////check in tableController if the index is already given
            //for (var j = 0; j < i; j++)
            //{
            //    while (randomIndex == givenYindex[j] && givenYindex.Length < tableCollumCount)
            //        randomIndex = UnityEngine.Random.Range(0, tableCollumCount);
            //}

            //givenYindex[i] = randomIndex;
            //var value = LinearFunction.LinearFunction.GetY(tabelController.GetXValue(randomIndex), _slope, _yIntersept);
            //Debug.Log("Random index: " + randomIndex);
            //Debug.Log("Random value: " + value);
            //tabelController.SetYValue(randomIndex, value);

            var value = LinearFunction.LinearFunction.GetY(tabelController.GetXValue(i), _slope, _yIntersept);
            tabelController.SetYValue(i, value);
        }
    }
}
