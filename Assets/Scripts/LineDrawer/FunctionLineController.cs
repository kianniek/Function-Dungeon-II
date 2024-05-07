using System;
using UnityEngine;

namespace LineController
{
    [ExecuteInEditMode]
    public class FunctionLineController : MonoBehaviour
    {
        [Header("Function settings")]
        public bool isQuadratic = false;

        [Header("Function coefficients")]
        [Tooltip("This variable defines the slope of the function.")]
        [SerializeField] private float a = 1f;
        [Tooltip("This variable defines the y-intercept of the function.")]
        [SerializeField] private float b = 0f;
        [Tooltip("This variable defines the quadratic power of the function.")]
        [Min(0.001f)]
        [SerializeField] private float c = 1f;

        [Header("Visual settings")]
        [SerializeField] private float lineLength = 10f;
        public float LineLength
        {
            get { return lineLength; }
            private set
            {
                lineLength = Mathf.Max(0, value);  // Ensure lineLength never goes negative.
                DrawFunction();  // Update the line renderer whenever the value changes.
            }
        }

        [SerializeField] private int segments = 10;
        [SerializeField] private LineRenderer lineRenderer;



        private void Start()
        {
            DrawFunction();
        }

        private void Update()
        {
            DrawFunction();
        }
        /// <summary>
        /// Evaluates the function at a given 'x' value.
        /// </summary>
        /// <param name="x">The 'x' value at which to evaluate the function.</param>
        /// <returns>The 'y' value of the function at 'x'.</returns>
        public float EvaluateFunction(float x)
        {
            return a * Mathf.Pow(x, c) + b;
        }
        /// <summary>
        /// Computes the velocity at a point along the function.
        /// </summary>
        /// <param name="startPos">The starting position on the function.</param>
        /// <param name="followDistance">The distance along the function to calculate the velocity.</param>
        /// <returns>A normalized vector representing the velocity.</returns>
        public Vector3 GetVelocityAtPoint(Vector3 startPos, float followDistance)
        {
            var startX = startPos.x;
            var endX = startX + followDistance;
            var startY = EvaluateFunction(startX);
            var endY = EvaluateFunction(endX);

            return new Vector3(endX - startX, endY - startY, 0).normalized;
        }
        /// <summary>
        /// Draws the function line based on the current coefficients and settings.
        /// </summary>
        private void DrawFunction()
        {
            if (!isQuadratic)
            {
                c = 1; // Forcing 'c' to be 1 if not quadratic
            }
            if (lineRenderer == null)
            {
                Debug.LogError("LineRenderer is missing.");
                return;
            }
            lineRenderer.positionCount = segments + 1;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;

            var step = lineLength / segments;
            for (var i = 0; i <= segments; i++)
            {
                var x = i * step;
                var y = EvaluateFunction(x);
                lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }
        }

        private void OnValidate()
        {
            DrawFunction();
        }
    }
}
