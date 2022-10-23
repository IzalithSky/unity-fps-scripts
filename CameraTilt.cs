using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTilt : MonoBehaviour {
    public Camera cam;

    public float amount = 4f;
    public float maxamount = 4f;
    public float smooth = 4f;
    
    Quaternion def;

    void Start() {
        def = cam.transform.localRotation;
    }

    void Update() {
        float factorZ = -Input.GetAxis("Horizontal") * amount;
        factorZ = Mathf.Clamp(factorZ, -maxamount, maxamount);
        
        Quaternion final = Quaternion.Euler(0, 0, def.z + factorZ);

        cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, final, Time.deltaTime * amount * smooth);  
    } 
}
