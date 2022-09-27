using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float timeoutSec = 4;

    private void OnCollisionEnter (Collision c) {
//         Destroy(gameObject);
//         Destroy(c.gameObject);
    }

    void Start() {
        Destroy(gameObject, timeoutSec);
    }
}
