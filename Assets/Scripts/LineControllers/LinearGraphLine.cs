using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace LineControllers
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class LinearGraphLine : MonoBehaviour
    {
        private const int Resolution = 10;
        
        [SerializeField, Min(1)] private float length = 10f;
        [SerializeField] private UnityEvent onLineUpdate = new();
        
        private LineRenderer _lineRenderer;
        private float _a;
        private float _b;
        
        /// <summary>
        /// The coefficient of the x-term in the linear function.
        /// </summary>
        public float A
        {
            set
            {
                _a = value;
                
                UpdateLine();
            }
            
            get => _a;
        }
        
        /// <summary>
        /// The constant term in the linear function.
        /// </summary>
        public float B
        {
            set
            {
                _b = value;
                
                UpdateLine();
            }
            
            get => _b;
        }
        
        /// <summary>
        /// Updates the line with the given x-coefficient and constant term.
        /// </summary>
        /// <param name="a"> The x-coefficient. </param>
        /// <param name="b"> The constant term. </param>
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
            
            for (var i = 0; i < Resolution; i++)
            {
                var x = i * (length / Resolution);
                var y = MathExtensions.LinearFunctionY(_a, _b, x);
                
                _lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }
            
            onLineUpdate.Invoke();
        }
    }
}