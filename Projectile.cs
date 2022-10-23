using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float timeoutSec = 4;
    public GameObject impactFlash;
    public Light lt;

    private void OnCollisionEnter (Collision c) {
        GameObject impfl = Instantiate(impactFlash, c.contacts[0].point, Quaternion.LookRotation(c.contacts[0].normal));
        Destroy(impfl, impfl.GetComponent<ParticleSystem>().main.duration);
        
        // Destroy(gameObject);
        // Destroy(c.gameObject);
    }

    void Start() {
        // if (null != lt) {
        //     lt.color = new Color(
        //         Random.Range(128, 255), 
        //         Random.Range(128, 255), 
        //         Random.Range(128, 255),
        //         255f);
        // }

        Destroy(gameObject, timeoutSec);
    }
}
