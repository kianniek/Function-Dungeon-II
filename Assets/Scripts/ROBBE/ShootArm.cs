using Events.GameEvents.Typed;
using UnityEngine;

public class ShootArm : MonoBehaviour
{
    //[SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Vector2 shootPoint;
    [SerializeField] private float projectileSpeed = 5f;
    private float _aValue;
    private float _bValue;
    [SerializeField] private Vector2GameEvent onHandShot;

    // TODO create projectile
    void Start()
    {
        //var projecile = Instantiate(projectilePrefab);
        //_projecileTransform = projecile.transform;

        //hide the projectile until it is shot
    }

    public void Shoot()
    {
        onHandShot.Invoke(new Vector2(_aValue, _bValue));
    }

    public void GetAValue(float aValue)
    {
        _aValue = aValue;
    }

    public void GetBValue(float bValue)
    {
        _bValue = bValue;
    }
}
