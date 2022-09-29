using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun1 : MonoBehaviour
{
    public Transform firePoint;
    public ParticleSystem muzzleFlash;
    public GameObject impactFlash;

    public float fireRateRps = 2;

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

            muzzleFlash.Play();

            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit)) {
                GameObject impfl = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impfl, impfl.GetComponent<ParticleSystem>().main.duration);

//                 Debug.Log(hit.transform.name);
//                 Destroy(hit.transform.gameObject);
            }
        }
    }
}
