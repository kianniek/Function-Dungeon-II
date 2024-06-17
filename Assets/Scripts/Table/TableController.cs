using System.Collections.Generic;
using System.Linq;
using Events;
using Events.GameEvents.Typed;
using LinearFunction;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Table
{
    public class TableController : MonoBehaviour
    {
        private const string PlaceholderText = " ";
        
        [Header("Data")] 
        [SerializeField] private LinearFunctionData linearFunctionData;
        [SerializeField] private int amoutGivenValues;
        
        [Header("Events")] 
        [SerializeField] private FloatGameEvent onInputChanged;
        [SerializeField] private ExtendedButtonGameEvent onExtendedButtonClicked;
        [SerializeField] private ExtendedButton currentSelectedButton;
        
        [Header("Check Settings")] 
        [SerializeField] private float checkMargin;
        [SerializeField] private UnityEvent onAllValuesSet = new();
        [SerializeField] private BoolEvent onInputCorrect;
        [SerializeField] private BoolEvent onInputIncorrect;
        
        [Header("Table Buttons")] 
        [SerializeField] private List<Button> tableXButtons = new();
        [SerializeField] private List<ExtendedButton> tableYButtons = new();
        
        private readonly Dictionary<Button, TMP_Text> _tableXDictionary = new();
        private readonly Dictionary<ExtendedButton, TMP_Text> _tableYDictionary = new();
        
        /// <summary>
        /// Gets the number of columns (buttons) in the table.
        /// </summary>
        public int ColumnCount => tableXButtons.Count;
        
        /// <summary>
        /// Gets the number of Y values in the table that are given values.
        /// </summary>
        public int AmountGivenValues => amoutGivenValues;
        
        private void Awake()
        {
            _tableXDictionary.Clear();
            _tableYDictionary.Clear();
            
            foreach (var button in tableXButtons)
            {
                _tableXDictionary[button] = button.GetComponentInChildren<TMP_Text>();
            }
            
            foreach (var extendedButton in tableYButtons)
            {
                _tableYDictionary[extendedButton] = extendedButton.GetComponentInChildren<TMP_Text>();
            }
            
            linearFunctionData.ValidateSlopeAndYIntercept();
            
            ResetYTexts();
        }
        
        private void OnEnable()
        {
            //Add the listeners
            onInputChanged.AddListener(OnInputChanged);
            onExtendedButtonClicked.AddListener(OnButtonClicked);
        }
        
        private void OnDisable()
        {
            //Remove the listeners
            onInputChanged.RemoveListener(OnInputChanged);
            onExtendedButtonClicked.RemoveListener(OnButtonClicked);
        }
        
        private void Start()
        {
            //Check if the table rows are the same size
            if (tableXButtons.Count != tableYButtons.Count)
                Debug.LogError("The two table rows are not the same size in columns");
        }
        
        private void OnButtonClicked(ExtendedButton extendedButton)
        {
            currentSelectedButton = extendedButton;
        }
        
        private void OnInputChanged(float value)
        {
            if (!currentSelectedButton)
                return;
            
            //Set the value of the current selected button
            currentSelectedButton.ButtonValue = value;
            _tableYDictionary[currentSelectedButton].text = $"{value}";
            
            //check if all the y values are set by looking if button
            var allSet = tableYButtons.All(button => button.ButtonText.text != PlaceholderText);
            
            if(allSet)
                onAllValuesSet?.Invoke();
        }
        
        /// <summary>
        /// Gets the Y values from the extended buttons.
        /// </summary>
        /// <returns>An array of Y values.</returns>
        public float[] GetYValues()
        {
            return tableYButtons.Select(button => button.ButtonValue).ToArray();
        }
        
        public float GetXButtonValue(int index)
        {
            //Get the x value from the table buttons
            return float.Parse(_tableXDictionary[tableXButtons[index]].text);
        }
        
        public void SetYButtonValue(int index, float value)
        {
            var button = tableYButtons[index];
            
            _tableYDictionary[button].text = $"{value}";
            
            button.ButtonValue = value;
            button.SetButtonInteractable(false);
        }
        
        public void ResetYTexts()
        {
            //Reset the text of the table buttons
            foreach (var text in _tableYDictionary.Values)
            {
                text.text = PlaceholderText;
            }
        }
        
        public void ValidateInput()
        {
            var correct = true;
            
            //Get the correct values from the LinearFunctionData ScriptableObject
            var tableData = linearFunctionData.CorrectTableValues;
            
            //Get the values from the table buttons
            var xValues = linearFunctionData.GetXValues;
            var yValues = GetYValues();
            
            for (var i = 0; i < tableXButtons.Count; i++)
            {
                //Get the x and y values from the table buttons
                var xValue = xValues.Values[i];
                var yValue = yValues[i];
                
                //Check if the x value is in the table data
                if (!tableData.TryGetValue(xValue, out var correctValue))
                {
                    //Debug.LogError($"The x value {xValue} is not in the table data");
                    continue;
                }
                
                //Check if the y value is within the margin of the correct value
                if (!(yValue < correctValue - checkMargin) && !(yValue > correctValue + checkMargin))
                    continue;
                
                correct = false;
                
                break;
            }
            
            //Invoke the correct or incorrect event
            if (correct)
            {
                onInputCorrect?.Invoke(false);
            }
            else
            {
                onInputIncorrect?.Invoke(true);
            }
        }
    }
}