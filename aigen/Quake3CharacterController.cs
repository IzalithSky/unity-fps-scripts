using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Quake3CharacterController : MonoBehaviour
{
    public float walkSpeed = 6.0f;
    public float sprintSpeed = 9.0f;
    public float jumpForce = 8.0f;
    public float airControl = 1.0f;
    public float groundAcceleration = 20.0f;
    public float groundFriction = 6.0f;
    public float mouseSensitivity = 100.0f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private Transform mainCamera;
    private bool isGrounded;
    private float speed;
    private Vector3 moveDirection;
    private float xRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main.transform;
        speed = walkSpeed;

        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CameraRotation();
        PlayerInput();
    }

    void FixedUpdate()
    {
        ApplyGroundFriction();
        MoveCharacter();
    }

    void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    void PlayerInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0, vertical);
        moveDirection.Normalize();

        isGrounded = Physics.Raycast(transform.position, Vector3.down, GetComponent<CapsuleCollider>().bounds.extents.y - 0.1f, groundMask);
        Debug.Log("isGrounded " + isGrounded);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }

    void ApplyGroundFriction()
    {
        if (isGrounded)
        {
            Vector3 groundVelocity = rb.velocity;
            groundVelocity.y = 0;
            rb.velocity = Vector3.Lerp(groundVelocity, Vector3.zero, groundFriction * Time.fixedDeltaTime);
        }
    }

    void MoveCharacter()
    {
        Vector3 targetVelocity = transform.TransformDirection(moveDirection) * speed;
        targetVelocity.y = rb.velocity.y;

        if (isGrounded)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, groundAcceleration * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity += transform.TransformDirection(moveDirection) * airControl;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -speed, speed), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -speed, speed));
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            Debug.Log("jumping");
        }
    }
}
