using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable : Damageable
{
    public override void Die() {
        Debug.Log("oof");
    }
}
