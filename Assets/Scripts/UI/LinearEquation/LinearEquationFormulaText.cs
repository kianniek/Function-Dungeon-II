using LinearFunction;
using TMPro;
using UnityEngine;

/// <summary>
/// Linear equation formula text class that displays the linear equation formula
/// </summary>
public class LinearEquationFormulaText : MonoBehaviour
{
    [SerializeField] private LinearFunctionData linearFunctionData;
    
    private const string TextFormat = "y = {0}x + {1}";
    private const string GenericTextFormat = "y = ax + b";
    
    private TMP_Text _text;
    private string outputText;
    private float _slope;
    private float _yIntercept;
    private bool _useGenericTextFormat;
    
    /// <summary>
    /// Gets or sets the use generic text format
    /// </summary>
    public bool UseGenericTextFormat
    {
        get => _useGenericTextFormat;
        set
        {
            _useGenericTextFormat = value;
            SetText();
        }
    }
    
    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
    
    // Set the text
    private void SetText()
    {
        outputText = UseGenericTextFormat ? GenericTextFormat : TextFormat;
        _text.text = string.Format(outputText, _slope, _yIntercept);
    }
    
    /// <summary>
    /// Pulls the linear function data from the LinearFunctionData scriptable object
    /// </summary>
    public void PullLinearFunctionData()
    {
        _slope = linearFunctionData.Slope;
        _yIntercept = linearFunctionData.YIntercept;
        SetText();
    }
}