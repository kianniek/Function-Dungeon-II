using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class InputFieldFloatListener : MonoBehaviour
{
    private TMP_InputField _text;
    public FloatEvent OnValueChanged;

    void Start()
    {
        _text = GetComponent<TMP_InputField>();
        _text.onValueChanged.AddListener(InvokeValueChangeFloat);
    }

    private void InvokeValueChangeFloat(string value)
    {
        if (float.TryParse(value, out var parsedValue))
        {
            OnValueChanged.Invoke(parsedValue);
        }
    }
}
