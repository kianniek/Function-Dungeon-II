using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _collider;

    [SerializeField]
    private float gravity = 1f, friction = 2f, mass = 1f, bounciness;

    private PhysicsMaterial2D _physicsMaterial;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _physicsMaterial = new PhysicsMaterial2D();
        _physicsMaterial.bounciness = bounciness;
        _rb.sharedMaterial = _physicsMaterial;   

        _collider = GetComponent<Collider2D>();
        _physicsMaterial = new PhysicsMaterial2D();

        _physicsMaterial.bounciness = bounciness;
        _collider.sharedMaterial = _physicsMaterial;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.gravityScale = gravity;
        _rb.angularDrag = friction;
        _rb.mass = mass;
    }
}
