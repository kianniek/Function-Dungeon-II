using UnityEngine;

namespace LinearFunction
{
    [CreateAssetMenu(fileName = "LinearFunctionData", menuName = "Linear Function/LinearFunctionData", order = 1)]
    public class LinearFunctionData : ScriptableObject
    {
        [SerializeField] private float _slope;
        [SerializeField] private float _yIntercept;
        [SerializeField] private int _amountOfDecimals = 2;

        public float Slope => _slope;
        public float YIntercept => _yIntercept;
        public int AmountOfDecimals => _amountOfDecimals;

        public void SetSlope(float slope)
        {
            _slope = slope;
        }

        public void SetYIntercept(float yIntercept)
        {
            _yIntercept = yIntercept;
        }

        public void SetAmountOfDecimals(int amountOfDecimals)
        {
            _amountOfDecimals = amountOfDecimals;
        }
    }
}
