using UnityEngine;
using UnityEngine.Events;

namespace Cannon
{
    /// <summary>
    /// Controls the vertical movement of the cannon platform.
    /// </summary>
    public class LerpedYTranslation : MonoBehaviour
    {
        [SerializeField] private float maxHeight = 10f; // Maximum height the platform can reach.
        [SerializeField] private float minHeight; // Minimum height the platform can descend to.
        [SerializeField] private float movementSmoothing = 5f; // Smoothing factor for the platform movement.
        [SerializeField] private bool baseOffStartPosition; // Base movement on the position of the start

        [SerializeField] private UnityEvent onMovingUp = new (); // Event invoked when the platform is moving up.
        [SerializeField] private UnityEvent onMovingDown = new (); // Event invoked when the platform is moving down.
        [SerializeField] private UnityEvent onMoving = new (); // Event invoked when the platform is moving.

        private Vector3 _wantedPosition;
        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
            _wantedPosition = transform.position;
        }

        private void FixedUpdate()
        {
            Vector3 lerpedPosition;

            // Smoothly move the platform towards the wanted position.
            if (Vector3.Distance(transform.position, _wantedPosition) > 0.01f)
            {
                lerpedPosition = Vector3.Lerp(transform.position, _wantedPosition, Time.deltaTime * movementSmoothing);
                onMoving.Invoke();
            }
            else
            {
                lerpedPosition = _wantedPosition;
            }

            transform.position = lerpedPosition;
        }

        /// <summary>
        /// Move the platform vertically based on the input.
        /// </summary>
        /// <param name="input">Vertical input for moving the platform, typically from -1 to 1.</param>
        public void Move(float input)
        {
            // Clamping the position to ensure the platform stays within defined bounds.
            _wantedPosition.y = Mathf.Clamp(input, minHeight, maxHeight) + _startPosition.y;
        }

        /// <summary>
        /// Moves the platform upward by a specified amount.
        /// </summary>
        /// <param name="amount">The amount to move up, should be a positive number.</param>
        public void MoveUp(float amount)
        {
            Move(amount);
            
            onMovingUp.Invoke();
        }

        /// <summary>
        /// Moves the platform downward by a specified amount.
        /// </summary>
        /// <param name="amount">The amount to move down, should be a positive number.</param>
        public void MoveDown(float amount)
        {
            Move(-amount);
            
            onMovingDown.Invoke();
        }
    }
}