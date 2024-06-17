using System.Collections.Generic;
using System.Linq;
using Events;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace MineCart
{
    public class MineCartTrack : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float maxTrackLenght = 10f; // The max length the track can stretch to
        [SerializeField] private Vector2 trackConnectionPoint;

        [Header("Events")]
        [SerializeField] private GameObjectEvent onMineCartTrackplaced = new();
        [SerializeField] private UnityEvent onMineCartTrackPlacable = new();
        [SerializeField] private UnityEvent onMineCartTrackNotPlacable = new();

        public Vector2 LeftConnectionPoint { get; private set; } // The track on the left
        public Vector2 RightConnectionPoint { get; private set; } // The track on the right

        private float _y;

        private static float GetY(float angle)
        {
            return Mathf.Tan(angle * Mathf.Deg2Rad);
        }

        private void Start()
        {
            UpdateConnectionPoints();
            onMineCartTrackplaced.Invoke(gameObject);
        }

        public void CheckCollision()
        {
            if (Physics2D.OverlapPoint(transform.position))
            {
                onMineCartTrackNotPlacable.Invoke();
            }
            else
            {
                onMineCartTrackPlacable.Invoke();
            }
        }


        /// <summary>
        /// Creates two connection points to connect other tracks to
        /// </summary>
        public void UpdateConnectionPoints()
        {
            var position = new Vector2(transform.position.x, transform.position.y);

            LeftConnectionPoint = new Vector2(Mathf.Round(position.x - trackConnectionPoint.x), position.y - trackConnectionPoint.y) - new Vector2(0, _y / 2);
            RightConnectionPoint = new Vector2(Mathf.Round(position.x + trackConnectionPoint.x), position.y + trackConnectionPoint.y) + new Vector2(0, _y / 2);

            LeftConnectionPoint = new Vector2(MathExtensions.RoundValue(LeftConnectionPoint.x, 1), MathExtensions.RoundValue(LeftConnectionPoint.y, 1));
            RightConnectionPoint = new Vector2(MathExtensions.RoundValue(RightConnectionPoint.x, 1), MathExtensions.RoundValue(RightConnectionPoint.y, 1));
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

        //draws the connection points in the editor
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(LeftConnectionPoint, 0.1f);
            Gizmos.DrawSphere(RightConnectionPoint, 0.1f);
        }
    }
}