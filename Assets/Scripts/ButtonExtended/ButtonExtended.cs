using Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ButtonExtended
{
    [RequireComponent(typeof(Button))]
    public class ExtendedButton : Button
    {
        [SerializeField] private ButtonEvent onClickButton = new ButtonEvent();
        [SerializeField] private FloatEvent onClickFloat = new FloatEvent();

        private float _buttonValue;

        public float ButtonValue
        {
            get { return _buttonValue; }
            set { _buttonValue = value; }
        }

        public ButtonEvent OnClickButton
        {
            get { return onClickButton; }
            set { onClickButton = value; }
        }

        protected override void Start()
        {
            base.Start();
            // Ensure the original Button's onClick event also triggers this event
            onClick.AddListener(() => OnClickButton.Invoke(this));
            onClick.AddListener(() => onClickFloat.Invoke(_buttonValue));
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            // Invoke the custom event
            onClickButton.Invoke(this);
            onClickFloat.Invoke(_buttonValue);
        }

    }
}
