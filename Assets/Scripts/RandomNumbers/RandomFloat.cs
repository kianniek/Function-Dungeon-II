using Events;
using UnityEngine;

namespace RandomNumbers
{
    [ExecuteInEditMode]
    public class RandomFloat : MonoBehaviour
    {
        [SerializeField] private bool executeAtStart;
        [SerializeField] private int amountBehindComma;
        [SerializeField] private float minValue;
        [SerializeField] private float maxValue;
        [SerializeField] private FloatEvent onValueChanged;

        private void Start()
        {
            if (executeAtStart)
                GenerateRandomFloat();
        }

        public void GenerateRandomFloat()
        {
            var randomValue = Random.Range(minValue, maxValue);
            randomValue = Mathf.Round(randomValue * Mathf.Pow(10, amountBehindComma)) / Mathf.Pow(10, amountBehindComma);
            onValueChanged?.Invoke(randomValue);
        }
    }
}