using System;
using ButtonExtended;
using Events.GameEvents.Typed;
using LinearFunction;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Table
{
    public class TabelController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private LinearFunctionData linearFunctionData;
        [SerializeField] private Button[] tabelXButtons;
        [SerializeField] private ExtendedButton[] tabelYButtons;

        [Header("Events")]
        [SerializeField] private FloatGameEvent onInputChanged;
        [SerializeField] private ButtonGameEvent onButtonClicked;
        [SerializeField] private ExtendedButton _currentSelectedButton;

        [Header("Check Settings")]
        [SerializeField] private float checkMargin;
        [SerializeField] private UnityEvent onInputCorrect;
        [SerializeField] private UnityEvent onInputIncorrect;

        private TMP_Text[] _tabelXTexts;
        private TMP_Text[] _tabelYTexts;

        private void OnEnable()
        {
            onInputChanged.AddListener(OnInputChanged);
            onButtonClicked.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            onInputChanged.RemoveListener(OnInputChanged);
            onButtonClicked.RemoveListener(OnButtonClicked);
        }

        private void Awake()
        {
            _tabelXTexts = new TMP_Text[tabelXButtons.Length];
            _tabelYTexts = new TMP_Text[tabelYButtons.Length];

            for (var i = 0; i < tabelXButtons.Length; i++)
            {
                _tabelXTexts[i] = tabelXButtons[i].GetComponentInChildren<TMP_Text>();
                _tabelYTexts[i] = tabelYButtons[i].GetComponentInChildren<TMP_Text>();
            }
        }

        private void Start()
        {
            if (tabelXButtons.Length != tabelYButtons.Length)
            {
                Debug.LogError("The two tables are not the same size");
                return;
            }
        }

        public void OnButtonClicked(Button button)
        {
            _currentSelectedButton = button as ExtendedButton;
        }

        public void OnInputChanged(float value)
        {
            if (_currentSelectedButton == null)
                return;

            _currentSelectedButton.ButtonValue = value;
            var index = Array.IndexOf(tabelYButtons, _currentSelectedButton);
            _tabelYTexts[index].text = value.ToString();
        }

        public int GetColumnCount()
        {
            return tabelXButtons.Length;
        }

        public int GetXValue(int index)
        {
            return int.Parse(_tabelXTexts[index].text);
        }

        public void SetYValue(int index, float value)
        {
            _tabelYTexts[index].text = value.ToString();
            tabelYButtons[index].ButtonValue = value;
        }

        public void ResetYTexts()
        {
            foreach (var text in _tabelYTexts)
            {
                text.text = "N";
            }
        }

        public float[] GetYValues()
        {
            var values = new float[tabelYButtons.Length];
            for (var i = 0; i < tabelYButtons.Length; i++)
            {
                values[i] = tabelYButtons[i].ButtonValue;
            }
            return values;
        }

        public float[] GetXValues()
        {
            var values = new float[tabelXButtons.Length];
            for (var i = 0; i < tabelXButtons.Length; i++)
            {
                values[i] = Mathf.Round(float.Parse(_tabelXTexts[i].text) * Mathf.Pow(10, linearFunctionData.AmountOfDecimals)) / Mathf.Pow(10, linearFunctionData.AmountOfDecimals);
            }
            return values;
        }

        public void CheckInput()
        {
            var correct = true;
            for (var i = 0; i < tabelXButtons.Length; i++)
            {
                var xValue = GetXValues()[i];
                var yValue = GetYValues()[i];
                var correctValue = Mathf.Round(LinearFunctionHelper.GetY(xValue, linearFunctionData.Slope, linearFunctionData.YIntercept) * Mathf.Pow(10, linearFunctionData.AmountOfDecimals)) / Mathf.Pow(10, linearFunctionData.AmountOfDecimals);

                if (yValue < correctValue - checkMargin || yValue > correctValue + checkMargin)
                {
                    correct = false;
                    break;
                }
            }

            if (correct)
            {
                Debug.Log("Correct");
                onInputCorrect?.Invoke();
            }
            else
            {
                Debug.Log("Incorrect");
                onInputIncorrect?.Invoke();
            }
        }
    }
}
