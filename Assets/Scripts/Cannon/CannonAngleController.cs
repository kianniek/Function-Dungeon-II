using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonAngleController : MonoBehaviour
{
    [SerializeField]
    private GameObject _barrelRotationPivot;

    [SerializeField]
    private float a;

    private void FixedUpdate()
    {
        Rotate();
    }

    // Rotate the barrel to the specified angle
    public void Rotate()
    {
        _barrelRotationPivot.transform.rotation = Quaternion.Euler(0f, 0f, GetAngle(a));
    }

    // Calculate the angle in degrees
    private float GetAngle(float a)
    {
        float x = 1;
        return Mathf.Atan2(a * x, x) * Mathf.Rad2Deg;
    }
}