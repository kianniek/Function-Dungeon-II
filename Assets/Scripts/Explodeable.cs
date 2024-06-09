using Extensions;
using UnityEngine;

public class Explodeable : MonoBehaviour
{
    [SerializeField] private float forceRadius;
    [SerializeField] private bool applyForcePowerFallOff;
    [SerializeField] private float forcePower;
    
    private Collider2D[] _hitColliders;
    private Rigidbody2D _rigidBody2D;
    
    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }
    
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