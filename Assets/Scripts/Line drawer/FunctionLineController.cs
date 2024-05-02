using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class FunctionLineController : MonoBehaviour
{
    [Header("Function settings")]
    public bool isQuadratic = false;

    [Header("Function coefficients")]
    [Tooltip("This is varable defines <b>the slope</b> of the function")]
    [SerializeField] private float a = 1f;
    [Tooltip("This is varable defines the <b>y-intersept</b> of the function")]
    [SerializeField] private float b = 0f;
    [Tooltip("This is varable defines the <b>quadratic power</b> of the function")]
    [Min(0.001f)]
    [SerializeField] private float c = 1f;

    [Header("Visual settings")]
    [SerializeField] private float lineLength = 10f;
    [SerializeField] private int segments = 10;

    [SerializeField] private LineRenderer lineRenderer;

    void Start()
    {
        DrawFunction();
    }

    void Update()
    {
        // If you want the function to update dynamically in the game, you can call DrawFunction() here
        DrawFunction();
    }

    public void SetCoefficientA(float newA)
    {
        a = newA;
        DrawFunction();
    }

    public float GetCoefficientA()
    {
        return a;
    }

    public void SetCoefficientB(float newB)
    {
        b = newB;
        DrawFunction();
    }

    public void SetCoefficientC(float newC)
    {
        if (!isQuadratic)
        {
            Debug.LogWarning("Setting Coefficient C has no effect since this function is not <b>Quadratic</b> /n setting Coefficient C to <b>1</b>");
            c = 1;
            DrawFunction();
            return;
        }
    }

    public void SetLineLength(float newLength)
    {
        lineLength = newLength;
        DrawFunction();
    }

    public void SetSegments(int newSegments)
    {
        segments = newSegments;
        DrawFunction();
    }

    public float EvaluateFunction(float x)
    {
        return a * Mathf.Pow(x, c) + b;
    }

    public Vector3 GetVelocityAtPoint(Vector3 startPos, float followDistance)
    {
        float startX = startPos.x;
        float endX = startX + followDistance;

        float startY = EvaluateFunction(startX);
        float endY = EvaluateFunction(endX);

        return new Vector3(endX - startX, endY - startY, 0).normalized;
    }
    private void DrawFunction()
    {
        if (!isQuadratic)
        {
            //forcing c to be 1
            c = 1;
        }
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is missing");
            return;
        }
        lineRenderer.positionCount = segments + 1;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        float x;
        float y;
        float step = lineLength / segments;

        for (int i = 0; i <= segments; i++)
        {
            x = i * lineLength / segments;
            y = EvaluateFunction(x);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }

    internal float GetLineLength()
    {
        return lineLength;
    }
}
