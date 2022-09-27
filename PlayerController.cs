using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cam;

    public float sens = 800f;
    public float mfrc = 50f;
    public float bfactor = 15f;
    public float jfrc = 400f;
    public float maxrspd = 10f;
    public float maxwspd = 4f;
    public float aircdelay = 0.2f;
    public float jdelay = 0.2f;
    public int fpsCap = 60;

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
        yRotate -= Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        yRotate = ClampAngle(yRotate, minAngle, maxAngle);
        cam.transform.localRotation = Quaternion.Euler(yRotate, 0.0f, 0.0f);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sens * Time.deltaTime);


        moveDir = (rb.transform.right * Input.GetAxisRaw("Horizontal") + rb.transform.forward * Input.GetAxisRaw("Vertical")).normalized * mfrc;
        maxspd = (Input.GetAxisRaw("Fire3") == 0f) ? maxrspd : maxwspd; // walk/run
        isJumping = Input.GetAxisRaw("Jump") != 0f;
    }

    void FixedUpdate() {
        Vector3 forceTotal = Vector3.zero;

        bool hasAirCountrol = (Time.time - totime) <= aircdelay;
        if (grounded || hasAirCountrol) {
            if (moveDir == Vector3.zero || rb.velocity.magnitude >= maxspd) {
                if (grounded) {
                    rb.drag = bfactor;
                }
            } else {
                rb.drag = defaultDrag;
                forceTotal.x += moveDir.x;
                forceTotal.z += moveDir.z;
            }
        }

        bool canJump = grounded && (Time.time - jtime) > jdelay;
        if (canJump) {
            if (isJumping) {
                jtime = Time.time;
                forceTotal.y += jfrc;
            }
        }

        if (forceTotal != Vector3.zero) {
            rb.AddForce(forceTotal, ForceMode.Force);
        }
    }

     void OnCollisionStay (Collision c) {
         grounded = true;
     }

     void OnCollisionExit (Collision c) {
         grounded = false;
         totime = Time.time;
     }
}
