using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class CannonController : MonoBehaviour
{

    private float _angle;

    [SerializeField]
    private GameObject _barrelObject;

    public float angle
    {
        get { return _angle; }
        set
        {
            if (_angle != value)
            {
                _angle = value;
                OnAngleChanged();
            }
        }
    }

    private float _height;

    private float height
    {
        get { return _height; }
        set
        {
            if (_height != value)
            {
                _height = value;
                OnHeightChanged();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PointTowardsTarget(new Vector2(2,1));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void PointTowardsTarget(Vector2 targetCoordinates)
    {
        Vector2 direction = (targetCoordinates - new Vector2(transform.position.x, transform.position.y)).normalized;
        float angleRadians = Mathf.Atan2(direction.y, direction.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        _barrelObject.transform.rotation = Quaternion.Euler(0f, 0f, angleDegrees);
    }

    void OnAngleChanged()
    {
        //transform.position = Vector3.up * height;
    }   
    
    void OnHeightChanged()
    {
        transform.position += Vector3.up * height;
    }  
}
