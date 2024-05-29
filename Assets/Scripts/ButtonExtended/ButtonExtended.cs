using Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ButtonExtended
{
    [RequireComponent(typeof(Button))]
    public class ExtendedButton : MonoBehaviour
    {
        private Button button;
        [SerializeField] private ExtendedButtonEvent onClickButton = new ();
        [SerializeField] private FloatEvent onClickFloat = new ();
        private float _buttonValue;

        public Button Button => button;

        public float ButtonValue
        {
            get => _buttonValue;
            set => _buttonValue = value;
        }

        private ExtendedButtonEvent OnClickButton
        {
            get => onClickButton;
            set => onClickButton = value;
        }

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        protected void Start()
        {
            button.onClick.AddListener(() => OnClickButton.Invoke(this));
            button.onClick.AddListener(() => onClickFloat.Invoke(_buttonValue));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClickButton.Invoke(this);
            onClickFloat.Invoke(_buttonValue);
        }
    }
}
