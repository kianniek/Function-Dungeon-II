using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float speed = 10f; 
    [SerializeField] private float maxDistance = 20f; 
    [SerializeField] private float gravityScale = 1f; 
    [SerializeField] private float damage = 10f;
    [SerializeField] private float resetTime = 5f;

    private float _distanceTraveled; 
    private Vector3 _initialPosition;
    private Vector3 _lastPosition; 
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _initialPosition = transform.position;
        _rb.velocity = transform.forward * speed;
        _rb.gravityScale = 0;
    }

    private void FixedUpdate()
    {
        CalculateDistanceTraveled();
        CalculateDeactivationTimer();
    }

    /// <summary>
    /// Calculates the amount of distance the projectile has traveled, used to enable gravity after a certain distance
    /// </summary>
    private void CalculateDistanceTraveled()
    {
        var displacement = transform.position - _lastPosition;
        _distanceTraveled += displacement.magnitude;

        _lastPosition = transform.position;

        if (_distanceTraveled >= maxDistance)
        {
            EnablesGravity();
        }
    }

    /// <summary>
    /// Enables gravity by specifying a value other than zero
    /// </summary>
    private void EnablesGravity()
    {
        _rb.gravityScale = gravityScale;
    }

    /// <summary>
    /// Calculates the time before the objects gets deactavted due to inactivity (not Moving)
    /// </summary>
    private void CalculateDeactivationTimer()
    {
        if (_rb.velocity.magnitude <= 0.1f)
        {
            resetTime -= Time.deltaTime;
            if (resetTime <= 0f)
            {
                ResetAndDeactivate();
            }
        }
        else
        {
            resetTime = 5f;
        }
    }

    /// <summary>
    /// Resets the variables to their default and deactivates the gameObject
    /// </summary>
    private void ResetAndDeactivate()
    {
        _distanceTraveled = 0f;
        _rb.velocity = Vector3.zero;
        transform.position = _initialPosition;
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnablesGravity();
    }

    /// <summary>
    /// Launches the object towards a specified direction
    /// </summary>
    /// <param name="direction">The direction the projectile will be shot to</param>
    public void Shoot(Vector2 direction)
    {
        _rb.AddForce(direction.normalized * speed);
    }
}
