using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2 : MonoBehaviour {
    public Transform firePoint;
    public GameObject impactFlash;
    public GunAnimation anim;

    public float fireRateRps = 2;
    public float range = 1;
    public float splash = .2f;

    private bool ready = true;
    private float t1;
    private bool fireing = false;

    void Update() {
        fireing = Input.GetAxis("Fire1") != 0;
    }

    void FixedUpdate() {
        if (fireing) {
            Fire();
        }
    }

    public void Fire() {
        if (!ready) {
            if ((Time.time - t1) >= (1 / fireRateRps)) {
                ready = true;
            }
        }

        if (ready) {
            t1 = Time.time;
            ready = false;

            anim.Fire();

            RaycastHit hit;
            if (Physics.SphereCast(firePoint.position, splash, firePoint.forward, out hit, range)) {
                GameObject impfl = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impfl, impfl.GetComponent<ParticleSystem>().main.duration);

//                 Debug.Log(hit.transform.name);
//                 Destroy(hit.transform.gameObject);
            }
        }
    }
}
