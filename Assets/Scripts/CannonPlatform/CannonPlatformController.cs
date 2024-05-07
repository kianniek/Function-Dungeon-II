using UnityEngine;
using UnityEngine.Events;
namespace CannonPlatform
{
    /// <summary>
    /// Controls the vertical movement of the cannon platform.
    /// </summary>
    public class CannonPlatformController : MonoBehaviour
    {
        [SerializeField]
        private float maxHeight = 10f; // Maximum height the platform can reach.
        [SerializeField]
        private float minHeight = 0f; // Minimum height the platform can descend to.
        [SerializeField]
        UnityEvent OnMovingUp; // Event invoked when the platform is moving up.
        [SerializeField]
        UnityEvent OnMovingDown; // Event invoked when the platform is moving down.

        private Vector3 _wantedPosition;

        private void Start()
        {
            _wantedPosition = transform.position;
        }

        private void Update()
        {
            // Smoothly move the platform towards the wanted position.
            if (Vector3.Distance(transform.position, _wantedPosition) > 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, _wantedPosition, Time.deltaTime * 5f);
            }
            else
            {
                transform.position = _wantedPosition;
            }

        }


        /// <summary>
        /// Move the platform vertically based on the input.
        /// </summary>
        /// <param name="input">Vertical input for moving the platform, typically from -1 to 1.</param>
        public void Move(float input)
        {
            float translation = input;
            Vector3 newPosition = _wantedPosition + new Vector3(0, translation, 0);

            // Clamping the position to ensure the platform stays within defined bounds.
            newPosition.y = Mathf.Clamp(newPosition.y, minHeight, maxHeight);

            _wantedPosition = newPosition;
        }

        /// <summary>
        /// Moves the platform upward by a specified amount.
        /// </summary>
        /// <param name="amount">The amount to move up, should be a positive number.</param>
        public void MoveUp(float amount)
        {
            Move(amount);
            OnMovingUp.Invoke();
        }

        /// <summary>
        /// Moves the platform downward by a specified amount.
        /// </summary>
        /// <param name="amount">The amount to move down, should be a positive number.</param>
        public void MoveDown(float amount)
        {
            Move(-amount);
            OnMovingDown.Invoke();
        }
    }
}