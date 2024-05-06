
using UnityEngine;
using Unity;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.Pool;

public class CannonProjectileController : MonoBehaviour
{

    private List<GameObject> _pooledProjectiles;

    [SerializeField]
    private GameObject prefabToPool;

    [SerializeField]
    private int amountToPool = 20;

    [SerializeField]
    public UnityEvent OnCannonFire;

    public GameObject GetPooledProjectile()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!_pooledProjectiles[i].activeInHierarchy)
            {
                return _pooledProjectiles[i];
            }
        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        _pooledProjectiles = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(prefabToPool);
            tmp.SetActive(false);
            _pooledProjectiles.Add(tmp);
        }
    }

    public void ShootProjectile()
    {
        GameObject projectile = GetPooledProjectile();
        if (projectile)
        {
            projectile.transform.position = transform.position;
            projectile.SetActive(true);
            OnCannonFire.Invoke();
        }
        else
        {
            Debug.LogWarning("Not enough pooled projectiles! Consider increasing the current ammount: " + amountToPool);
        }
    }
}
