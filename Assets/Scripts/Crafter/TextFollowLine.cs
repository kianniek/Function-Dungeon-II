using UnityEngine;

public class TextFollowLine : MonoBehaviour
{
    private LineRenderer _lineToFollow;
    private int _indexOfPoint = 2;
    private float _zPosition = -0.5f;
    private float _yPositionOffset;

    private void Awake()
    {
        _lineToFollow = GetComponentInParent<LineRenderer>();
    }

    private void Start()
    {
        _yPositionOffset = transform.parent.position.y;
        transform.position = new Vector3(_lineToFollow.GetPosition(_indexOfPoint).x, _lineToFollow.GetPosition(_indexOfPoint).y + _yPositionOffset, _zPosition);
    }
}
