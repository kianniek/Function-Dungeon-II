using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] 
    private float speed = 10f; 

    [SerializeField] 
    private float maxDistance = 20f; 

    [SerializeField] 
    private float gravity = 9.81f; 

    [SerializeField] 
    private float damage = 10f;

    [SerializeField] 
    private float resetTime = 5f;

    private float _distanceTraveled = 0f; 
    private Vector3 _initialPosition;
    private Vector3 _lastPosition; 
    private Rigidbody2D _rb;

    private void Start()
    {
        _initialPosition = transform.position;
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.forward * speed;
        _rb.gravityScale = 0;
    }

    private void Update()
    {
        Vector3 displacement = transform.position - _lastPosition;
        _distanceTraveled += displacement.magnitude;

        _lastPosition = transform.position;

        if (_distanceTraveled >= maxDistance)
        {
            _rb.gravityScale = gravity;
        }

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

  
    private void ResetAndDeactivate()
    {
        _distanceTraveled = 0f;
        _rb.velocity = Vector3.zero;
        transform.position = _initialPosition;
        gameObject.SetActive(false);
    }
}
