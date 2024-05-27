using UnityEngine;
using UnityEngine.InputSystem;

namespace Player2D
{
    public class PlayerController2D : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private float interactableCheckRadius = 0.2f;
        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform interactibleCheck;

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

        void FixedUpdate()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);

            _rb.velocity = new Vector2(_moveInput.x * moveSpeed, _rb.velocity.y);

            var collidersInRange = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius);

            foreach (var gameObjects in collidersInRange)
            {
                //gameObject.gameObject.GetComponent<i>
            }



            if (!_isGrounded)
            {
                Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);
            }
        }

        void OnDrawGizmos()
        {
            // Draw ground check radius in the editor
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {



        }
    }
}