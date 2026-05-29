using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MouseLookMovement : MonoBehaviour
{
    public float walkSpeed = 3.5f;
    public float runSpeed = 7.0f;
    public float acceleration = 10f;
    public float jumpForce = 5f;
    public float groundCheckDistance = 0.4f;
    public LayerMask groundMask;

    public float mouseSensitivity = 100f;
    public Transform cameraTransform; // Camera of the Player (set via Inspector)
    public Transform cameraHolder;    // The holder of the camera (for smooth rotation)

    public float currentSpeed;
    public Vector3 currentVelocity;
    public float xRotation = 0f;

    public Rigidbody rb;
    public bool isGrounded;
    public Vector3 inputDir;
    public bool isMoving;
    public bool isRunning;
    public StateScript stateScript;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();

        // Prevent the Rigidbody from rotating on the X and Z axes
        rb.freezeRotation = true;

        // Optionally, lock cursor to the center
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
        

        stateScript = GameObject.Find("State Manager").GetComponent<StateScript>();
    }

    void Update()
    {
        if (stateScript.state != State.Play) return;
        HandleMouseLook();
        HandleMovementInput();
        HandleJump();
        GroundCheck();
    }

    void FixedUpdate()
    {
        if (stateScript.state != State.Play) return;
        MovePlayer();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update X rotation for camera (clamped for vertical rotation)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the camera rotation (smooth transition)
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apply player body rotation (smooth horizontal rotation around Y-axis)
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovementInput()
    {
        // Getting input for horizontal and vertical movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Normalize input to avoid diagonal speed boost
        inputDir = new Vector3(horizontal, 0f, vertical).normalized;

        // Determine target speed based on shift key (running or walking)
        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 5f); // Smooth speed transition
    }

    void MovePlayer()
    {
        // Convert input direction to world space and apply speed
        Vector3 moveDirection = transform.TransformDirection(inputDir) * currentSpeed;

        // Apply horizontal velocity (no need to modify Y for gravity)
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveDirection.x;
        velocity.z = moveDirection.z;

        // Apply force to Rigidbody for realistic movement
        rb.linearVelocity = velocity; // Directly set velocity instead of adding force
        isMoving = inputDir.magnitude > 0.1f;
    }

    void HandleJump()
    {
        // Handle jumping (only allowed if grounded)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void GroundCheck()
    {
        // Raycast downwards to check if we are on the ground
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);
    }
}
