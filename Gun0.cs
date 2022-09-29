using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun0 : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireForce = 200f;

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

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
                proj.GetComponent<Rigidbody>().AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        }
    }
}
