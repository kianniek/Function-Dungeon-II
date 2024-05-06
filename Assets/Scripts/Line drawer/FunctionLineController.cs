using System;
using UnityEngine;

/// <summary>
/// Controls the visualization of a mathematical function using a LineRenderer in Unity.
/// </summary>
[ExecuteInEditMode]
public class FunctionLineController : MonoBehaviour
{
    [Header("Function settings")]
    public bool IsQuadratic = false;

    [Header("Function coefficients")]
    [Tooltip("This variable defines <b>the slope</b> of the function.")]
    [SerializeField] private float a = 1f;
    [Tooltip("This variable defines the <b>y-intercept</b> of the function.")]
    [SerializeField] private float b = 0f;
    [Tooltip("This variable defines the <b>quadratic power</b> of the function.")]
    [Min(0.001f)]
    [SerializeField] private float c = 1f;

    [Header("Visual settings")]
    [SerializeField] private float lineLength = 10f;
    [SerializeField] private int segments = 10;

    [SerializeField] private LineRenderer lineRenderer;

    /// <summary>
    /// Initializes and draws the function line.
    /// </summary>
    private void Start()
    {
        DrawFunction();
    }

    /// <summary>
    /// Dynamically updates the function line in the editor or game.
    /// </summary>
    private void Update()
    {
        DrawFunction();
    }

    /// <summary>
    /// Sets the coefficient 'a' of the function and redraws the function line.
    /// </summary>
    /// <param name="newA">The new value for coefficient 'a'.</param>
    public void SetCoefficientA(float newA)
    {
        a = newA;
        DrawFunction();
    }

    /// <summary>
    /// Returns the coefficient 'a'.
    /// </summary>
    /// <returns>The value of coefficient 'a'.</returns>
    public float GetCoefficientA()
    {
        return a;
    }

    /// <summary>
    /// Sets the coefficient 'b' of the function and redraws the function line.
    /// </summary>
    /// <param name="newB">The new value for coefficient 'b'.</param>
    public void SetCoefficientB(float newB)
    {
        b = newB;
        DrawFunction();
    }

    public float GetCoefficientB()
    {
        return b;
    }

    /// <summary>
    /// Sets the coefficient 'c' if the function is quadratic and redraws the function line; logs a warning if not quadratic.
    /// </summary>
    /// <param name="newC">The new value for coefficient 'c'.</param>
    public void SetCoefficientC(float newC)
    {
        if (!IsQuadratic)
        {
            Debug.LogWarning("Setting Coefficient C has no effect since this function is not <b>Quadratic</b>. Setting Coefficient C to <b>1</b>.");
            c = 1;
            DrawFunction();
            return;
        }
        c = newC;
    }

    /// <summary>
    /// Sets the length of the line and redraws the function line.
    /// </summary>
    /// <param name="newLength">The new length for the line.</param>
    public void SetLineLength(float newLength)
    {
        lineLength = newLength;
        DrawFunction();
    }

    /// <summary>
    /// Sets the number of segments for the line and redraws the function line.
    /// </summary>
    /// <param name="newSegments">The new number of segments.</param>
    public void SetSegments(int newSegments)
    {
        segments = newSegments;
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
    /// 
    public Vector3 GetVelocityAtPoint(Vector3 startPos, float followDistance)
    {
        float startX = startPos.x;
        float endX = startX + followDistance;

        float startY = EvaluateFunction(startX);
        float endY = EvaluateFunction(endX);

        return new Vector3(endX - startX, endY - startY, 0).normalized;
    }

    /// <summary>
    /// Draws the function line based on the current coefficients and settings.
    /// </summary>
    private void DrawFunction()
    {
        if (!IsQuadratic)
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

        float step = lineLength / segments;
        for (int i = 0; i <= segments; i++)
        {
            float x = i * step;
            float y = EvaluateFunction(x);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    /// <summary>
    /// Returns the length of the line.
    /// </summary>
    /// <returns>The length of the line.</returns>
    internal float GetLineLength()
    {
        return lineLength;
    }

    /// <summary>
    /// Draw the function line in the editor.
    /// </summary>
    /// 
    private void OnValidate()
    {
        DrawFunction();
    }
}
