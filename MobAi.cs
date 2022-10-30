using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobAi : MonoBehaviour {
    public NavMeshAgent nm;
    public GameObject player;
    public Transform toolHolder;
    public Tool tool;
    
    void Update() {
        nm.SetDestination(player.transform.position);
        toolHolder.LookAt(player.transform);
    }

    void FixedUpdate() {
        if (Vector3.Distance(player.transform.position, transform.position) <= nm.stoppingDistance) {
            tool.Fire();
        }
    }
}
