using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAnimation : MonoBehaviour
{
    public Animator anim;
    public string idleAnimName;
    public string walkAnimName;

    public void Idle() {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName(idleAnimName)) {
            anim.Play(idleAnimName, 0, 0);
        }
    }

    public void Walk() {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName(walkAnimName)) {
            anim.Play(walkAnimName, 0, 0);
        }
    }
}
