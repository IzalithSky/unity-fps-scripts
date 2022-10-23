using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSway : MonoBehaviour {
    public float amount = 15f;
    public float maxamount = 15f;
    public float smooth = 15f;
    public float rightOffset = .3f;
    
    Quaternion defaultRotation;
    Vector3 defaultPosition;

    void  Start (){
        defaultRotation = transform.localRotation;
        defaultPosition = transform.localPosition;
    }

    void  Update (){
        float fx = -Input.GetAxis("Vertical") * amount;
        float fy = -Input.GetAxis("Horizontal") * amount;
        float fz = -Input.GetAxis("Horizontal") * amount;
        float fr = -Input.GetAxis("Horizontal") * amount;
        float ff = -Input.GetAxis("Vertical") * amount;
        
        fx = Mathf.Clamp(fx, -maxamount * .3f, maxamount *.3f);
        fy = Mathf.Clamp(fy, -maxamount, maxamount);
        fz = Mathf.Clamp(fz, -maxamount, maxamount);
        fr = Mathf.Clamp(fr, -rightOffset, rightOffset);
        ff = Mathf.Clamp(ff, -rightOffset * .3f, rightOffset * .3f);

        Quaternion finalRotation = Quaternion.Euler(defaultRotation.x + fx, defaultRotation.y + fy, defaultRotation.z + fz);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation, Time.deltaTime * amount * smooth);

        Vector3 finalPosition = new Vector3(defaultPosition.x + fr, defaultPosition.y, defaultPosition.z + ff);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition, Time.deltaTime * rightOffset * smooth);
    }
}
