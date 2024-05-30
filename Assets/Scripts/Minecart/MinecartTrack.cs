using Events;
using UnityEngine;
using UnityEngine.Events;

public class MinecartTrack : MonoBehaviour
{
    [SerializeField] private Transform leftTrackCheck;
    [SerializeField] private Transform rightTrackCheck;

    [SerializeField] private GameObject _leftMinecartTrack;
    [SerializeField] private GameObject _rightMinecartTrack;

    [SerializeField] private float maxTrackLenght = 10f;

    [Header("Events")]
    [SerializeField] private UnityEvent onMinecartTrackplaced = new();

    private void FixedUpdate()
    {
        AdjustLength();
    }

    private void CheckConnection()
    {
        var leftCollider = Physics2D.OverlapPoint(leftTrackCheck.position);
        var rightCollider = Physics2D.OverlapPoint(rightTrackCheck.position);

        _leftMinecartTrack = leftCollider != null ? leftCollider.gameObject : null;
        _rightMinecartTrack = rightCollider != null ? rightCollider.gameObject : null;
    }

    private void AdjustLength()
    {
        var angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        var adjustedLength = Mathf.Clamp(1f / Mathf.Cos(angle), 0, maxTrackLenght);

        Vector2 newSize = transform.localScale;
        newSize.x = adjustedLength;
        transform.localScale = newSize;
    }

    public void PlaceMinecartTrack()
    {
        CheckConnection();
        onMinecartTrackplaced.Invoke();
    }
}
