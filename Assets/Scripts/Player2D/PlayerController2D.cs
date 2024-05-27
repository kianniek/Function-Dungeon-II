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

        private Interactable _nearbyInteractable;
        private Rigidbody2D _rb;
        private Vector2 _moveInput;
        private bool _isGrounded;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnMove(InputAction.CallbackContext context)
        {
            _moveInput = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (_isGrounded && context.action.WasPressedThisFrame())
                _rb.velocityY = jumpForce;
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if(_nearbyInteractable != null)
            {
                _nearbyInteractable.InvokeInteraction();
            }
        }

        void FixedUpdate()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);

            _rb.velocity = new Vector2(_moveInput.x * moveSpeed, _rb.velocity.y);

            var collidersInRange = Physics2D.OverlapCircleAll(interactableCheck.position, interactableCheckRadius);

            foreach (var collider2D in collidersInRange)
            {
                var interactable = collider2D.gameObject.GetComponent<Interactable>();
                if(interactable != null)
                {
                    _nearbyInteractable = interactable;
                    break;
                }
            }

            if (!_isGrounded)
            {
                Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);
            }
        }

        void OnDrawGizmos()
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