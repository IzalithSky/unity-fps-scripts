using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public float walkSpeed = 3.0f;
    public float sprintSpeed = 6.0f;
    public float crouchSpeed = 1.5f;
    public float jumpForce = 8.0f;
    public float gravity = 30.0f;
    public float crouchHeight = 0.5f;
    public float standHeight = 2.0f;
    public float crouchTime = 0.5f;

    private CharacterController controller;
    private Transform mainCamera;
    private float speed;
    private Vector3 moveDirection = Vector3.zero;
    private bool isCrouching = false;
    private float originalCameraHeight;
    private float targetCameraHeight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main.transform;
        speed = walkSpeed;
        originalCameraHeight = mainCamera.localPosition.y;
        targetCameraHeight = originalCameraHeight;
    }

    void Update()
    {
        Move();
        Crouch();
    }

    private void Move()
    {
        if (controller.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            moveDirection = new Vector3(horizontal, 0.0f, vertical);
            moveDirection = transform.TransformDirection(moveDirection);

            // Sprint
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
            }
            else if (isCrouching)
            {
                speed = crouchSpeed;
            }
            else
            {
                speed = walkSpeed;
            }

            moveDirection *= speed;

            // Jump
            if (Input.GetButtonDown("Jump") && !isCrouching)
            {
                moveDirection.y = jumpForce;
            }
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            targetCameraHeight = isCrouching ? crouchHeight : standHeight;
            controller.height = isCrouching ? crouchHeight : standHeight;
        }

        float currentCameraHeight = mainCamera.localPosition.y;
        currentCameraHeight = Mathf.Lerp(currentCameraHeight, targetCameraHeight, Time.deltaTime / crouchTime);
        mainCamera.localPosition = new Vector3(mainCamera.localPosition.x, currentCameraHeight, mainCamera.localPosition.z);
    }
}
