using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _collider;

    [SerializeField]
    private float _gravity = 1f, _friction = 2f, _mass = 1f, _bounciness;

    private PhysicsMaterial2D physicsMaterial;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        physicsMaterial = new PhysicsMaterial2D();
        physicsMaterial.bounciness = _bounciness;
        _rb.sharedMaterial = physicsMaterial;   

        _collider = GetComponent<Collider2D>();
        physicsMaterial = new PhysicsMaterial2D();

        physicsMaterial.bounciness = _bounciness;
        _collider.sharedMaterial = physicsMaterial;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.gravityScale = _gravity;
        _rb.angularDrag = _friction;
        _rb.mass = _mass;
    }
}
