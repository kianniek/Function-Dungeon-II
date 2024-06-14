using Events;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ExtendedButton : MonoBehaviour
    {
        [SerializeField] private ExtendedButtonEvent onClickParseButton = new();
        [SerializeField] private FloatEvent onClickFloat = new();
        
        [SerializeField] private TMP_Text buttonText;
        
        private Button _button;
        
        public float ButtonValue { get; set; }
        
        public TMP_Text ButtonText
        {
            get
            {
                if (buttonText) 
                    return buttonText;
                
                buttonText = GetComponentInChildren<TMP_Text>();
                
                if (!buttonText)
                {
                    Debug.LogError("TMP_Text component is not assigned and cannot be found in children.");
                }
                
                return buttonText;
            }
        }
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            
            if (!buttonText)
                buttonText = GetComponentInChildren<TMP_Text>();
            
            if (!buttonText)
                Debug.LogError($"TMP_Text component is missing on {gameObject.name}");
        }
        
        protected void Start()
        {
            _button.onClick.AddListener(() => onClickParseButton.Invoke(this));
            _button.onClick.AddListener(() => onClickFloat.Invoke(ButtonValue));
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            onClickParseButton.Invoke(this);
            onClickFloat.Invoke(ButtonValue);
        }
        
        public void SetButtonValue(float value)
        {
            SetButtonValue(value, Color.clear, false);
        }
        
        public void SetButtonValue(float value, Color color, bool useColor = true)
        {
            Debug.Log(_button);
            Debug.Log(gameObject.name);
            ButtonValue = value;
            
            if (ButtonText != null)
            {
                ButtonText.text = $"{value}";
            }
            else
            {
                Debug.LogError($"ButtonText is null on {gameObject.name} when trying to set value {value}");
            }
            
            if (useColor)
            {
                _button.colors = ColorBlockExtensions.GetColorBlock(color);
            }
        }
    }
}