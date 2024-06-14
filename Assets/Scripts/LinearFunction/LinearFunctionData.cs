using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace LinearFunction
{
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
        
        private readonly Dictionary<float, float> _correctTableValues = new();

        /// <summary>
        /// Gets or sets the slope of the linear function. If random values are used, generates a random slope.
        /// </summary>
        public float Slope { get; private set; }

        /// <summary>
        /// Gets or sets the y-intercept of the linear function. If random values are used, generates a random y-intercept.
        /// </summary>
        public float YIntercept { get; private set; }

        /// <summary>
        /// Gets the number of decimal places for rounding.
        /// </summary>
        public int AmountOfDecimals => amountOfDecimals;
        
        /// <summary>
        /// Gets a read-only dictionary of correct X and Y table values.
        /// </summary>
        public IReadOnlyDictionary<float, float> CorrectTableValues => _correctTableValues;
        
        public XValues GetXValues => tableXValues;
        
        public float MinSlope => minSlope;
        public float MaxSlope => maxSlope;
        
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
                Slope = slope;
                YIntercept = yIntercept;
            }
            else
            {
                Slope = GenerateRandomFloat(minSlope, maxSlope, amountOfDecimals);
                YIntercept = GenerateRandomFloat(minYIntercept, maxYIntercept, amountOfDecimals);
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
            _correctTableValues.Clear();
            
            foreach (var xValue in tableXValues.Values)
            {
                _correctTableValues[xValue] = MathExtensions.LinearFunctionY(xValue, Slope, YIntercept);
            }
        }
    }
}