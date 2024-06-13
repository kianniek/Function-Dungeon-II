using Events;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ExtendedButton : MonoBehaviour
    {
        [SerializeField] private ExtendedButtonEvent onClickParseButton = new ();
        [SerializeField] private FloatEvent onClickFloat = new ();
        
        public Button Button { get; private set; }
        
        public float ButtonValue { get; set; }

        private void Awake()
        {
            Button = GetComponent<Button>();
        }

        protected void Start()
        {
            Button.onClick.AddListener(() => onClickParseButton.Invoke(this));
            Button.onClick.AddListener(() => onClickFloat.Invoke(ButtonValue));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClickParseButton.Invoke(this);
            onClickFloat.Invoke(ButtonValue);
        }
        
        public void SetButtonValue(TMP_Text text, float value)
        {
            SetButtonValue(text, value, Color.clear, false);
        }
        
        public void SetButtonValue(TMP_Text text, float value, Color color, bool useColor = true)
        {
            ButtonValue = value;
            text.text = $"{value}";
            
            if (useColor)
            {
                Button.colors = ColorBlockExtensions.GetColorBlock(color);
            }
        }
    }
}
