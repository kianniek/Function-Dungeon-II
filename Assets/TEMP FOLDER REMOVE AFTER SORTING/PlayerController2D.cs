using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{    
    [SerializeField]  private float jumpForce = 10f;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField]  private Transform groundCheck;
    [SerializeField] private InputAction playerControls;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _isMoving;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _isMoving = _moveInput == Vector2.zero;
    }

    void FixedUpdate()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);

        _rb.velocity = new Vector2(_moveInput.x * moveSpeed, _rb.velocity.y);

        if (_isGrounded)
        {
            _isJumping = true;
        }
    }

    void OnDrawGizmos()
    {
        // Draw ground check radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
