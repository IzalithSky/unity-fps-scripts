using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float timeoutSec = 4;
    public GameObject impactFlash;
    public GameObject explosion;
    public Light lt;
    public GameObject bmark;
    public float bmarkTtl = 20f;
    public int bmarkLimit = 4;

    private void OnCollisionEnter (Collision c) {
        GameObject impfl = Instantiate(impactFlash, c.contacts[0].point, Quaternion.LookRotation(c.contacts[0].normal));
        Destroy(impfl, impfl.GetComponent<ParticleSystem>().main.duration);

        GameObject e1 = Instantiate(explosion, c.contacts[0].point, Quaternion.LookRotation(c.contacts[0].normal));
        Destroy(e1, 1f);

        // if (bmarkLimit > 0) {
        //     GameObject bm1 = Instantiate(bmark, c.contacts[0].point + (c.contacts[0].normal * .001f), Quaternion.FromToRotation(Vector3.up, c.contacts[0].normal));
        //     Destroy(bm1, bmarkTtl);
        //     bmarkLimit--;
        // }
        
        Debug.Log(c.gameObject.name);
        Destroy(gameObject);
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
