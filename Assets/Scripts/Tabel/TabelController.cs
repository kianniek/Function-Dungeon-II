using System;
using System.Collections;
using System.Collections.Generic;
using Events.GameEvents.Typed;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabelController : MonoBehaviour
{
    [SerializeField] private Button[] tabelXButtons;
    [SerializeField] private Button[] tabelYButtons;

    private TMP_Text[] tabelXTexts;
    private TMP_Text[] tabelYTexts;

    [SerializeField] private FloatGameEvent onInputChanged;
    [SerializeField] private ButtonGameEvent onButtonClicked;

    [SerializeField] private Button _currentSelectedButton;

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
        tabelXTexts = new TMP_Text[tabelXButtons.Length];
        tabelYTexts = new TMP_Text[tabelYButtons.Length];

        for (var i = 0; i < tabelXButtons.Length; i++)
        {
            tabelXTexts[i] = tabelXButtons[i].GetComponentInChildren<TMP_Text>();
            tabelYTexts[i] = tabelYButtons[i].GetComponentInChildren<TMP_Text>();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //if the two tables are not the same size, throw an error
        if (tabelXButtons.Length != tabelYButtons.Length)
        {
            Debug.LogError("The two tables are not the same size");
            return;
        }
    }

    public void OnButtonClicked(Button button)
    {
        _currentSelectedButton = button;
    }

    public void OnInputChanged(float value)
    {
        if (_currentSelectedButton == null)
            return;

        var index = Array.IndexOf(tabelYButtons, _currentSelectedButton);
        Debug.Log("Index: " + index);
        tabelYTexts[index].text = value.ToString();
    }

    /// <summary>
    /// Get the value of the x axis at the given index
    /// </summary>
    /// <returns></returns>
    public int GetCollumCount()
    {
        return tabelXButtons.Length;
    }

    /// <summary>
    /// Get the value of the x axis at the given index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetXValue(int index)
    {
        return int.Parse(tabelXTexts[index].text);
    }

    /// <summary>
    /// Set the value of the x axis at the given index
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public void SetYValue(int index, float value)
    {
        tabelYTexts[index].text = value.ToString();
    }
}
