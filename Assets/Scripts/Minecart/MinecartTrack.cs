using Events;
using UnityEngine;
using UnityEngine.Events;

namespace MinecartTrack
{
    public class MinecartTrack : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform leftTrackCheck; // The check for if the track is connected on the left
        [SerializeField] private Transform rightTrackCheck; // The check for if the track is connected on the right

        [Header("Settings")]
        [SerializeField] private float maxTrackLenght = 10f; // The max length the track can stretch to

        [Header("Events")]
        [SerializeField] private GameObjectEvent onMinecartTrackplaced = new();

         private GameObject _leftMinecartTrack; // The track on the left
         private GameObject _rightMinecartTrack; // The track on the right

        private void Start()
        {
            onMinecartTrackplaced.Invoke(gameObject);
        }

        private void CheckConnection()
        {
            var leftCollider = Physics2D.OverlapPoint(leftTrackCheck.position);
            var rightCollider = Physics2D.OverlapPoint(rightTrackCheck.position);

            _leftMinecartTrack = leftCollider != null ? leftCollider.gameObject : null;
            _rightMinecartTrack = rightCollider != null ? rightCollider.gameObject : null;
        }

        /// <summary>
        /// Changes the length of the track to ensure it can connect two tracks
        /// </summary>
        public void AdjustLength()
        {
            var angle = transform.eulerAngles.z * Mathf.Deg2Rad;
            var adjustedLength = Mathf.Clamp(1f / Mathf.Cos(angle), 0, maxTrackLenght);

            var newSize = transform.localScale;
            newSize.x = adjustedLength;
            transform.localScale = newSize;
        }

        /// <summary>
        /// Places the minecart and checks connection with other tracks
        /// </summary>
        public void PlaceMinecartTrack()
        {
            CheckConnection();
        }

    }
}