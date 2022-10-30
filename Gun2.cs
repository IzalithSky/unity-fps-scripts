using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2 : Tool {
    public Transform firePoint;
    public GameObject impactFlash;
    public GunAnimation anim;

    public float range = 1;
    public float splash = .2f;

    protected override void FireReady() {
        anim.Fire();

        RaycastHit hit;
        if (Physics.SphereCast(firePoint.position, splash, firePoint.forward, out hit, range)) {
            GameObject impfl = Instantiate(impactFlash, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impfl, impfl.GetComponent<ParticleSystem>().main.duration);

            // Debug.Log(hit.transform.name);
            // Destroy(hit.transform.gameObject);
        }   
    }
}
