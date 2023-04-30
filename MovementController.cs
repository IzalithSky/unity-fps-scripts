using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float maxrspd = 10f;
    public float maxwspd = 4f;
    public float aircdelay = 0.5f;
    public float jdelay = 0.2f;
    public float bfactor = 15f;
    public float jfrc = 15f;
    public float mfrc = 50f; 

    Rigidbody rb;
    InputListener il;

    bool grounded = false;
    float totime = 0f;
    float jtime = 0f;

    float defaultDrag = 0f;
    float maxspd = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultDrag = rb.drag;

        il = GetComponent<InputListener>();
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.Rotate(Vector3.up * il.GetCameraHorizontal());
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 moveDir = 
            (rb.transform.right * il.GetInputHorizontal() 
            + rb.transform.forward * il.GetInputVertical())
            .normalized * mfrc;
        maxspd = il.GetIsWalking() ? maxrspd : maxwspd; // walk/run

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
        if (grounded || hasAirCountrol) {
            if (moveDir != Vector3.zero && rb.velocity.magnitude < maxspd) {
                rb.AddForce(moveDir, ForceMode.Force);
            }
        }

        bool canJump = grounded && (Time.time - jtime) > jdelay;
        if (canJump) {
            if (il.GetIsJumping()) {
                jtime = Time.time;
                rb.AddForce(rb.transform.up * jfrc, ForceMode.Impulse);
            }
        }
    }
}