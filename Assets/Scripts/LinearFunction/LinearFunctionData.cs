using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LinearFunction
{
    /// <summary>
    /// Represents a collection of X values for the linear function.
    /// </summary>
    [Serializable]
    public struct XValues : IEnumerable<float>
    {
        private const int FixedLength = 6;
        [SerializeField] private float[] values;
        
        /// <summary>
        /// Constructor to initialize the XValues with a fixed number of float values.
        /// </summary>
        /// <param name="values">The X values.</param>
        public XValues(params float[] values)
        {
            this.values = new float[FixedLength];
            
            for (var i = 0; i < FixedLength; i++)
            {
                if (i < values.Length)
                    this.values[i] = values[i];
                else
                    this.values[i] = 0f; // or any default value you prefer
            }
        }
        
        /// <summary>
        /// Returns an enumerator that iterates through the X values.
        /// </summary>
        public IEnumerator<float> GetEnumerator()
        {
            return ((IEnumerable<float>)values).GetEnumerator();
        }
        
        //return a array of float values
        public float[] GetValues()
        {
            return values;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }
    }
    
    /// <summary>
    /// ScriptableObject that holds the data for a linear function, including slope, intercept, and related values.
    /// </summary>
    [CreateAssetMenu(fileName = "LinearFunctionData", menuName = "Linear Function/LinearFunctionData", order = 1)]
    public class LinearFunctionData : ScriptableObject
    {
        [SerializeField] private int amountOfDecimals = 1;
        [SerializeField] private bool useRandomValues;
        [SerializeField] private float slope;
        [SerializeField] private float yIntercept;
        [SerializeField] private XValues tableXValues;
        
        [Header("Random Slope Settings")] 
        [SerializeField] private float minSlope = -10;
        
        [SerializeField] private float maxSlope = 10;
        
        [Header("Random Y-Intercept Settings")] 
        [SerializeField] private float minYIntercept = -10;
        
        [SerializeField] private float maxYIntercept = 10;
        
        private readonly Dictionary<float, float> correctTableValues = new();
        
        private float _slope;
        private float _yIntercept;
        
        /// <summary>
        /// Gets or sets the slope of the linear function. If random values are used, generates a random slope.
        /// </summary>
        public float Slope => _slope;
        
        /// <summary>
        /// Gets or sets the y-intercept of the linear function. If random values are used, generates a random y-intercept.
        /// </summary>
        public float YIntercept => _yIntercept;
        
        /// <summary>
        /// Gets the number of decimal places for rounding.
        /// </summary>
        public int AmountOfDecimals => amountOfDecimals;
        
        /// <summary>
        /// Gets a read-only dictionary of correct X and Y table values.
        /// </summary>
        public IReadOnlyDictionary<float, float> CorrectTableValues => correctTableValues;
        
        public XValues GetXValues => tableXValues;
        
        public float MinSlope => minSlope;
        public float MaxSlope => maxSlope;
        public float MinYIntercept => minYIntercept;
        public float MaxYIntercept => maxYIntercept;
        
        
        
        /// <summary>
        /// Initializes the dictionaries mapping buttons to their text components and validates the slope and y-intercept values.
        /// </summary>
        private void Awake()
        {
            ValidateSlopeAndYIntercept();
        }
        
        /// <summary>
        /// Validates the slope and y-intercept values, generating random values if necessary, and fills the correct table values dictionary.
        /// </summary>
        public void ValidateSlopeAndYIntercept()
        {
            if (!useRandomValues)
            {
                _slope = slope;
                _yIntercept = yIntercept;
            }
            else
            {
                _slope = GenerateRandomFloat(minSlope, maxSlope, amountOfDecimals);
                _yIntercept = GenerateRandomFloat(minYIntercept, maxYIntercept, amountOfDecimals);
            }
            
            FillDictionary();
        }
        
        /// <summary>
        /// Generates a random float value within the specified range and rounds it to the specified number of decimal places.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        /// <returns>A random float value.</returns>
        private static float GenerateRandomFloat(float minValue, float maxValue, int decimalPlaces)
        {
            return Mathf.Round(Random.Range(minValue, maxValue) * Mathf.Pow(10, decimalPlaces)) /
                   Mathf.Pow(10, decimalPlaces);
        }
        
        /// <summary>
        /// Fills the dictionary with the correct table values based on the linear function.
        /// </summary>
        private void FillDictionary()
        {
            correctTableValues.Clear();
            
            foreach (var xValue in tableXValues)
            {
                correctTableValues[xValue] = LinearFunctionHelper.GetY(xValue, _slope, _yIntercept);
            }
        }
    }
}