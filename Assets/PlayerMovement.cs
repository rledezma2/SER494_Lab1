using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody rb;
    private PlayerControls controls;
    private Vector2 moveInput;
    private bool jumpPressed;

    private bool moveForward, moveBack, moveLeft, moveRight;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveForward = true;
        controls.Player.Move.canceled += ctx => moveForward = false;

        controls.Player.Down.performed += ctx => moveBack = true;
        controls.Player.Down.canceled += ctx => moveBack = false;

        controls.Player.Left.performed += ctx => moveLeft = true;
        controls.Player.Left.canceled += ctx => moveLeft = false;

        controls.Player.Right.performed += ctx => moveRight = true;
        controls.Player.Right.canceled += ctx => moveRight = false;

        controls.Player.Jump.performed += ctx => jumpPressed = true;
    }

    void OnEnable() => controls.Player.Enable();
    void OnDisable() => controls.Player.Disable();

    void Update()
    {
        moveInput.x = 0f;
        moveInput.y = 0f;

        if (moveRight) moveInput.x += 1f;
        if (moveLeft) moveInput.x -= 1f;
        if (moveForward) moveInput.y += 1f;
        if (moveBack) moveInput.y -= 1f;

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 newPosition = rb.position + move * moveSpeed * Time.fixedDeltaTime;
        
        if (move.magnitude > 0.1f)  
        {
            rb.linearVelocity = new Vector3(move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        if (jumpPressed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        jumpPressed = false;
    }

    

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}