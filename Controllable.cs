using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class
Controllable: MonoBehaviour {
    public CharacterController controller;
    public Transform cam;
    public Tool currentTool;

    public float sens = 800;
    public float wspd = 4;
    public float rspd = 8;
    public int fpsCap = 0;

    float rotationX = 0;

    Vector3 v;
    bool isGrounded;
    float spd = 2.0f;
    float jumpHeight = 1.0f;
    float gravity = -9.81f;
    bool fireing = false;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = fpsCap;
    }

    void Update() {
        fireing = Input.GetAxis("Fire1") != 0;

        spd = (Input.GetAxis("Fire2") != 0) ? wspd : rspd;
        isGrounded = controller.isGrounded;

        if (isGrounded && v.y < 0) {
            v.y = 0f;
        }

        Vector3 move = (transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") + transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical")).normalized;
        controller.Move(move * Time.deltaTime * spd);

        if (Input.GetAxis("Jump") != 0 && isGrounded) {
            v.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }

        v.y += gravity * Time.deltaTime;
        controller.Move(v * Time.deltaTime);
    }

    void LateUpdate() {
        rotationX += -Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        cam.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sens * Time.deltaTime, 0);
    }

    void FixedUpdate() {
        if (fireing && null != currentTool) {
            currentTool.Fire();
        }
    }
}
