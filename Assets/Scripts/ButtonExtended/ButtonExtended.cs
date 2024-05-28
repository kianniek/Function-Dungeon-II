using Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ButtonExtended
{
    [RequireComponent(typeof(Button))]
    public class ExtendedButton : Button
    {
        [SerializeField] private ButtonEvent onClickButton = new ();
        [SerializeField] private FloatEvent onClickFloat = new ();
        private float _buttonValue;

        public float ButtonValue
        {
            get => _buttonValue;
            set => _buttonValue = value;
        }

        public ButtonEvent OnClickButton
        {
            get => onClickButton;
            set => onClickButton = value;
        }

        protected override void Start()
        {
            base.Start();
            onClick.AddListener(() => OnClickButton.Invoke(this));
            onClick.AddListener(() => onClickFloat.Invoke(_buttonValue));
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            onClickButton.Invoke(this);
            onClickFloat.Invoke(_buttonValue);
        }
    }
}
