using TMPro;
using UnityEngine;

namespace Crafter
{
    public class LineToText : MonoBehaviour
    {
        private LineRenderer _lineToFollow;
        private GameObject _line;

        private float _a;
        private float _b;
        private int _indexOfPoint = 2;
        private float _zPosition = -0.5f;
        private float _yPositionOffset;

        private TextMeshPro _formulaText;

        private void Awake()
        {
            _lineToFollow = GetComponentInParent<LineRenderer>();
            _line = transform.parent.gameObject;
            _formulaText = GetComponent<TextMeshPro>();
            _a = _line.GetComponent<FunctionLineController>().A;
            _b = _line.transform.position.y;
        }

        void Start()
        {
            _formulaText.text = $"Y = {_a}x + {_b}"; 
            _yPositionOffset = transform.parent.position.y;
            transform.position = new Vector3(_lineToFollow.GetPosition(_indexOfPoint).x, _lineToFollow.GetPosition(_indexOfPoint).y + _yPositionOffset, _zPosition);
        }
    }
}