using UnityEngine;
using UnityEngine.InputSystem;

namespace Player2D
{
    public class PlayerController2D : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private float interactableCheckRadius = 0.5f;
        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform interactableCheck;

        [SerializeField] private GameObject objectToCreate;
        [SerializeField] private Transform positionToCreate;

        private Interactable _nearbyInteractable;
        private Rigidbody2D _rb;
        private Vector2 _moveInput;
        private bool _isGrounded;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// This method is used by the input system to control the players movement
        /// </summary>
        /// <param name="context">Provides the method with the current state of the keyboard input</param>
        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        /// <summary>
        /// This method is used by the input system to make the player jump using a button
        /// </summary>
        /// <param name="context">Holds the state of the button</param>
        public void OnJump(InputAction.CallbackContext context)
        {
            if (_isGrounded && context.action.WasPressedThisFrame())
                _rb.velocityY = jumpForce;
        }

        /// <summary>
        /// This method is used by the input system to allow the player to interact with the world
        /// </summary>
        /// <param name="context">Holds the state of the button</param>
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (_nearbyInteractable != null)
            {
                _nearbyInteractable.InvokeInteraction();
            }
        }

        /// <summary>
        /// This method is used by the input system to create an object at the position of the transform
        /// </summary>
        public void OnPlaceObject(InputAction.CallbackContext context)
        {
            if (context.action.WasPressedThisFrame())
            {
                Instantiate(objectToCreate, positionToCreate.position, Quaternion.identity);
            }
        }


        private void FixedUpdate()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);

            _rb.velocity = new Vector2(_moveInput.x * moveSpeed, _rb.velocity.y);

            var collidersInRange = Physics2D.OverlapCircleAll(interactableCheck.position, interactableCheckRadius);

            foreach (var collider2D in collidersInRange)
            {
                var interactable = collider2D.gameObject.GetComponent<Interactable>();
                if (interactable != null)
                {
                    _nearbyInteractable = interactable;
                    break;
                }
            }

            GroundCheck();
        }

        private void GroundCheck()
        {
            if (!_isGrounded)
            {
                Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);
            }
        }

        private void OnDrawGizmos()
        {
            // Draws ground check radius
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

            // Draws interactible check radius
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(interactableCheck.position, interactableCheckRadius);
        }
    }
}