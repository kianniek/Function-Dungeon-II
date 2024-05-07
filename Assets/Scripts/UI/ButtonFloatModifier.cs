using System;
using TypedUnityEvent;
using UnityEngine;

namespace UI
{
    public class ButtonFloatModifier : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float startValue;
        [SerializeField] private Vector2 range = new(-10f, 10f);
        [SerializeField] private float step = 0.1f;
        [SerializeField] private int roundings = 1;

        [Header("Events")] 
        [SerializeField] private FloatEvent onFloatChange = new();

        private float _value;

        public float Value
        {
            get => _value;

            private set
            {
                _value = Mathf.Clamp(MathF.Round(value, roundings), range.x, range.y);

                onFloatChange.Invoke(_value);
            }
        }

        private void Start()
        {
            Value = startValue;
        }

        public void Increase()
        {
            Value += step;
        }

        public void Decrease()
        {
            Value -= step;
        }
    }
}