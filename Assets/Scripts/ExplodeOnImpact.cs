using UnityEngine;
using Utils;

/// <summary>
/// This class is responsible for defining the behavior of an object that can explode.
/// </summary>
public class ExplodeOnImpact : MonoBehaviour
{
    [Header("Radius Settings")]
    [SerializeField] private float forceRadius;

    [Header("Power Settings")]
    [SerializeField] private float forcePower;
    [SerializeField] private bool applyForcePowerFallOff;
    
    private Collider2D[] _hitColliders;
    private Rigidbody2D _rigidBody2D;
    
    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
    /// <summary>
    /// Explodes the object and applies force to all colliders within the force radius.
    /// </summary>
    public void Explode()
    {
        _hitColliders = Physics2D.OverlapCircleAll(transform.position, forceRadius);
        
        foreach (var hitCollider in _hitColliders)
        {
            var attachedRigidBody = hitCollider.attachedRigidbody;
            
            if (!attachedRigidBody || attachedRigidBody == _rigidBody2D)
                continue;
            
            var colliderDirection = hitCollider.transform.position - transform.position;
            
            var forcePowerFalloff = applyForcePowerFallOff
                ? MathExtensions.CircleFallOff(colliderDirection.magnitude, forceRadius)
                : 1;
            
            attachedRigidBody.AddForce(colliderDirection.normalized * forcePower * forcePowerFalloff);
        }
    }
}