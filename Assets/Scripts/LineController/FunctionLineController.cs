using UnityEngine;

//MVC pattern 
namespace LineController
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(LineRenderer))]
    public class FunctionLineController : MonoBehaviour
    {
        [Header("Function settings")]
        public bool isQuadratic;
        
        [Header("Function coefficients")]
        [Tooltip("This variable defines the slope of the function.")]
        [SerializeField] private float a = 1f;
        [Tooltip("This variable defines the y-intercept of the function.")]
        [SerializeField] private float b;
        [Tooltip("This variable defines the quadratic power of the function.")]
        [Min(0.001f)]
        [SerializeField] private float c = 1f;

        [Header("Visual settings")]
        [SerializeField] private float lineLength = 10f;
        [SerializeField] private int segments = 10;
        [SerializeField] private LineRenderer lineRenderer;

        public float LineLength
        {
            get => lineLength;
            private set
            {
                lineLength = Mathf.Max(0, value);  // Ensure lineLength never goes negative.
                UpdateFunction();  // Update the line renderer whenever the value changes.
            }
        }

        /// <summary>
        /// Sets the value of 'a' and updates the function.
        /// </summary>
        public float A
        {
            get => a;
            set
            {
                a = value;
                UpdateFunction();
            }
        }

        /// <summary>
        /// Sets the value of 'b' and updates the function.
        /// </summary>
        public float B
        {
            get => b;
            set
            {
                b = value;
                UpdateFunction();
            }
        }

        /// <summary>
        /// Sets the value of 'b' and updates the function.
        /// </summary>
        public float C
        {
            get => c;
            set
            {
                c = value;
                UpdateFunction();
            }
        }
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
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
        /// </summary
        private void UpdateFunction()
        {
            if (!isQuadratic)
            {
                c = 1; // Forcing 'c' to be 1 if not quadratic
            }

            // Set the number of positions in the line renderer to match the number of segments and avoid exceeding the array bounds
            lineRenderer.positionCount = segments + 1;

            // Calculate the step size for each segment
            var step = lineLength / segments;
            // Iterate over each segment and evaluate the function
            for (var i = 0; i <= segments; i++)
            {
                // Calculate the x and y values for the function
                var x = i * step;
                var y = EvaluateFunction(x);

                // Set the position of the line renderer
                lineRenderer.SetPosition(i, new Vector3(x, y, 0));
            }
        }

        private void OnValidate()
        {
            UpdateFunction();
        }
    }
}
