using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootArm : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] Vector2[] projectilePositions;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private UnityEvent onTargetReached = new();
    
    private Transform _projecileTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        var projecile = Instantiate(projectilePrefab);
        _projecileTransform = projecile.transform;
        
        //hide the projectile until it is shot
    }
    
    public void Shoot()
    {
        StartCoroutine(GoTroughPositions());
    }
    
    /// <summary>
    /// Shoots the projectile to the target position and waits until it reaches the target position before moving to the next position
    /// </summary>
    /// <returns></returns>
    private IEnumerator GoTroughPositions()
    {
        foreach (var position in projectilePositions)
        {
            yield return ShootToTarget(position, shootPoint.position);
        }
    }
    
    /// <summary>
    /// Shoots the projectile to the target position
    /// </summary>
    /// <param name="target"> The target position </param>
    /// <param name="shootPoint"> The point where the projectile is shot from </param>
    /// <returns></returns>
    private IEnumerator ShootToTarget(Vector2 target, Vector2 shootPoint)
    {
            var projectile = Instantiate(projectilePrefab);
            projectile.transform.position = shootPoint;
            var direction = target - shootPoint;
            //make the projectile look at the target
            projectile.transform.right = direction;
            
            //while the projectile is not at the target keep moving
            while (Vector3.Distance(projectile.transform.position, target) > 0.1f)
            {
                projectile.transform.position = Vector2.MoveTowards(projectile.transform.position, target, projectileSpeed * Time.deltaTime);
                yield break;
            }
            
            projectile.transform.position = target;
            onTargetReached.Invoke();
            yield return null;
    }
}
