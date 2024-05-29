using TMPro;
using UnityEngine;

namespace Crafter
{
    public class LineToText : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineToFollow;

        [SerializeField] private int indexOfPointToFollow = 2;

        private Transform _line;

        private TextMeshPro _formulaText;

        private float _a;
        private float _b;
        private float _zPosition = -0.5f;
        private float _yPositionOffset;

        private void Awake()
        {
            _line = lineToFollow.transform;
            _formulaText = GetComponent<TextMeshPro>();
            _a = _line.GetComponent<FunctionLineController>().A;
            _b = _line.transform.position.y;
        }

        void Start()
        {
            _formulaText.text = $"Y = {_a}x + {_b}"; 
            _yPositionOffset = transform.parent.position.y;
            transform.position = new Vector3(lineToFollow.GetPosition(indexOfPointToFollow).x, lineToFollow.GetPosition(indexOfPointToFollow).y + _yPositionOffset, _zPosition);
        }
    }
}