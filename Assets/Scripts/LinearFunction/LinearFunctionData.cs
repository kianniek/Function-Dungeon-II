using UnityEngine;

namespace LinearFunction
{
    [CreateAssetMenu(fileName = "LinearFunctionData", menuName = "Linear Function/LinearFunctionData", order = 1)]
    public class LinearFunctionData : ScriptableObject
    {
        [SerializeField] private int amountOfDecimals = 1;
        
        [Header("Random Slope Settings")] 
        [SerializeField] private float minSlope = -10;
        [SerializeField] private float maxSlope = 10;
        
        [Header("Random Y-Intercept Settings")] 
        [SerializeField] private float minYIntercept = -10;
        [SerializeField] private float maxYIntercept = 10;
        
        public float Slope { private set; get; }
        
        public float YIntercept { private set; get; }
        
        public int AmountOfDecimals => amountOfDecimals;
        
        public void GenerateRandomSlopeAndYIntercept()
        {
            GenerateRandomSlope();
            GenerateRandomYIntercept();
        }
        
        public void GenerateRandomSlope()
        {
            Slope = GenerateRandomFloat(minSlope, maxSlope, amountOfDecimals);
        }
        
        public void GenerateRandomYIntercept()
        {
            YIntercept = GenerateRandomFloat(minYIntercept, maxYIntercept, amountOfDecimals);
        }
        
        private static float GenerateRandomFloat(float minValue, float maxValue, int amountBehindComma)
        {
            return Mathf.Round(Random.Range(minValue, maxValue) * 
                   Mathf.Pow(10, amountBehindComma)) /
                   Mathf.Pow(10, amountBehindComma);
        }
    }
}