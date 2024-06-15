using Events;
using Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Minecart
{
    public class MinecartTrack : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float maxTrackLenght = 10f; // The max length the track can stretch to
        [SerializeField] private Vector2 trackConnectionPoint;

        [Header("Events")]
        [SerializeField] private GameObjectEvent onMinecartTrackplaced = new();
        [SerializeField] private UnityEvent onMinecartTrackPlacable = new();
        [SerializeField] private UnityEvent onMinecartTrackNotPlacable = new();

        public Vector2 LeftConnectionPoint { get; private set; } // The track on the left
        public Vector2 RightConnectionPoint { get; private set; } // The track on the right

        private float _y;

        private static float GetY(float angle)
        {
            return Mathf.Tan(angle * Mathf.Deg2Rad);
        }

        private void Start()
        {
            SetConnectionPoint();
            onMinecartTrackplaced.Invoke(gameObject);
        }

        public void CheckCollision()
        {
            if (Physics2D.OverlapPoint(transform.position))
            {
                onMinecartTrackNotPlacable.Invoke();
            }
            else
            {
                onMinecartTrackPlacable.Invoke();
            }
        }


        /// <summary>
        /// Creates two connection points to connect other tracks to
        /// </summary>
        public void SetConnectionPoint()
        {
            var position = new Vector2(transform.position.x, transform.position.y);

            LeftConnectionPoint = new Vector2(Mathf.Round(position.x - trackConnectionPoint.x), position.y - trackConnectionPoint.y) - new Vector2(0, _y / 2);
            RightConnectionPoint = new Vector2(Mathf.Round(position.x + trackConnectionPoint.x), position.y + trackConnectionPoint.y) + new Vector2(0, _y / 2);

            LeftConnectionPoint = new Vector2(MathfExtentions.RoundValue(LeftConnectionPoint.x, 1), MathfExtentions.RoundValue(LeftConnectionPoint.y, 1));
            RightConnectionPoint = new Vector2(MathfExtentions.RoundValue(RightConnectionPoint.x, 1), MathfExtentions.RoundValue(RightConnectionPoint.y, 1));
        }

        /// <summary>
        /// Changes the length of the track to ensure it can connect two tracks
        /// </summary>
        public void AdjustLength(float slope)
        {
            _y = GetY(slope);

            var angle = slope * Mathf.Deg2Rad;
            var adjustedLength = Mathf.Clamp(1f / Mathf.Cos(angle), 0, maxTrackLenght);

            var newSize = transform.localScale;
            newSize.x = adjustedLength;
            transform.localScale = newSize;
        }

    }
}