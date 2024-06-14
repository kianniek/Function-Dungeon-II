using Extensions;
using UnityEngine;

namespace LineControllers
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class LinearGraphLine : MonoBehaviour
    {
        private const int Resolution = 2;
        
        [SerializeField, Min(1)] private float length = 10f;

        private LineRenderer _lineRenderer;
        private float _a;
        private float _b;

        public float A
        {
            set
            {
                _a = value;
                
                UpdateLine();
            }
        }

        public float B
        {
            set
            {
                _b = value;
                
                UpdateLine();
            }
        }
        
        public void UpdateLine(float a, float b)
        {
            _a = a;
            _b = b;
            
            UpdateLine();
        }
        
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void OnValidate()
        {
            UpdateLine();
        }

        private void UpdateLine()
        {
            if (!_lineRenderer)
                return;
            
            _lineRenderer.positionCount = Resolution;

            var direction = new Vector2 { x = 1, y = MathExtensions.LinearFunction(_a, _b, 1) }.normalized;
            var lineEndPoint = direction * length;

            _lineRenderer.SetPositions(new Vector3[] { Vector3.zero, lineEndPoint });
        }
    }
}