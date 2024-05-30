using UnityEngine;

namespace LinearFunction
{
    [CreateAssetMenu(fileName = "LinearFunctionData", menuName = "Linear Function/LinearFunctionData", order = 1)]
    public class LinearFunctionData : ScriptableObject
    {
        private float _slope;
        private float _yIntercept;

        [SerializeField] private int _amountOfDecimals = 2;

        [Header("Random Slope Settings")]
        [SerializeField] private float _minSlope = -10;
        [SerializeField] private float _maxSlope = 10;

        [Header("Random Y-Intercept Settings")]
        [SerializeField] private float _minYIntercept = -10;
        [SerializeField] private float _maxYIntercept = 10;

        public float Slope
        {
            private set => _slope = value;
            get => _slope;
        }
        public float YIntercept
        {
            private set => _yIntercept = value;
            get => _yIntercept;
        }
        public int AmountOfDecimals
        {
            private set => _amountOfDecimals = value;
            get => _amountOfDecimals;
        }

        public void GenerateRandomSlopeAndYIntercept()
        {
            GenerateRandomSlope();
            GenerateRandomYIntersept();
        }

        public void GenerateRandomSlope()
        {
            Slope = (GenerateRandomFloat(_minSlope, _maxSlope, _amountOfDecimals));
        }

        public void GenerateRandomYIntersept()
        {
            YIntercept = (GenerateRandomFloat(_minYIntercept, _maxYIntercept, _amountOfDecimals));
        }

        private float GenerateRandomFloat(float minValue, float maxValue, int amountBehindComma)
        {
            var randomValue = Random.Range(minValue, maxValue);
            randomValue = Mathf.Round(randomValue * Mathf.Pow(10, amountBehindComma)) / Mathf.Pow(10, amountBehindComma);
            return randomValue;
        }
    }
}
