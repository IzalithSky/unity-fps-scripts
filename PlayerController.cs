using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cam;
    public Tool currentTool;

    public float sens = 800f;
    public float mfrc = 50f;
    public float bfactor = 15f;
    public float jfrc = 15f;
    public float maxrspd = 10f;
    public float maxwspd = 4f;
    public float aircdelay = 0.5f;
    public float jdelay = 0.2f;
    public int fpsCap = 0;

    Vector3 moveDir = Vector3.zero;
    float maxspd = 0;
    bool isJumping = false;

    bool grounded = false;
    float totime = 0f;
    float jtime = 0f;

    float yRotate = 0.0f;
    float minAngle = -90f;
    float maxAngle = 90f;

    float defaultDrag = 0f;

    // Start is called before the first frame update
    void Start() {
        defaultDrag = rb.drag;

        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = fpsCap;
//         QualitySettings.vSyncCount = 1;
    }

    float ClampAngle(float lfAngle, float lfMin, float lfMax) {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    // Update is called once per frame
    void Update() {
        moveDir = (rb.transform.right * Input.GetAxisRaw("Horizontal") + rb.transform.forward * Input.GetAxisRaw("Vertical")).normalized * mfrc;
        maxspd = (Input.GetAxisRaw("Fire3") == 0f) ? maxrspd : maxwspd; // walk/run
        isJumping = Input.GetAxisRaw("Jump") != 0f;

        if (Input.GetAxis("Fire1") != 0 && null != currentTool) {
            currentTool.Fire();
        }
    }

    void LateUpdate() {
        yRotate -= Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        yRotate = ClampAngle(yRotate, minAngle, maxAngle);
        cam.transform.localRotation = Quaternion.Euler(yRotate, 0.0f, 0.0f);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sens * Time.deltaTime);
    }

    void FixedUpdate() {
        bool wasGrounded = grounded;
        grounded = Physics.Raycast(transform.position, Vector3.down, 1.2f);
        if (!grounded && wasGrounded) {
            totime = Time.time;
        }

        if (grounded && moveDir == Vector3.zero) {
            rb.drag = bfactor;
        } else {
            rb.drag = defaultDrag;
        }

        bool hasAirCountrol = (Time.time - totime) <= aircdelay;
        Debug.Log(Time.time + " " + totime + " " + hasAirCountrol);
        if (grounded || hasAirCountrol) {
            if (moveDir != Vector3.zero && rb.velocity.magnitude < maxspd) {
                rb.AddForce(moveDir, ForceMode.Force);
            }
        }

        bool canJump = grounded && (Time.time - jtime) > jdelay;
        if (canJump) {
            if (isJumping) {
                jtime = Time.time;
                rb.AddForce(rb.transform.up * jfrc, ForceMode.Impulse);
            }
        }
    }
}
