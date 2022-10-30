using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun0 : Tool {
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Transform lookPoint;
    public float fireForce = 200f;

    protected override void FireReady() {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, (null !=  lookPoint) ? lookPoint.rotation : firePoint.rotation);
        proj.GetComponent<Rigidbody>().AddForce(((null !=  lookPoint) ? lookPoint.forward : firePoint.forward) * fireForce, ForceMode.Impulse);
    }
}
