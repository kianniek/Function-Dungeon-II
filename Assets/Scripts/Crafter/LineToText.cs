using LineController;
using TMPro;
using UnityEngine;

public class LineToText : MonoBehaviour
{
    private GameObject _line;

    private float _a;
    private float _b;

    private TextMeshPro _formulaText;

    private void Awake()
    {
        _line = transform.parent.gameObject;
        _formulaText = GetComponent<TextMeshPro>();
        _a = _line.GetComponent<FunctionLineController>().A;
        _b = _line.transform.position.y;
    }

    void Start()
    {
        _formulaText.text = $"Y = {_a}x + {_b}";
    }
}
