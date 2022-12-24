using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {
    public float fireRateRps = 1f;

    protected bool ready = true;
    protected float t1;

    void Start () {
        t1 = Time.time; 
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

            FireReady();
        }
    }

    public bool IsReady() {
        if (!ready) {
            if ((Time.time - t1) >= (1 / fireRateRps)) {
                ready = true;
            }
        }
        return ready;
    }

    protected virtual void FireReady() {}
}
