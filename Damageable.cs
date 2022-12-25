using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {
    public int maxHp = 100;
    
    int hp = 0;

    void Start() {
        hp = maxHp;
    }

    public bool IsAlive() {
        return hp >= 0;
    }

    public void Hit(int damage) {
        if (damage > 0) {
            hp -= damage;
        }
        if (!IsAlive()) {
            Debug.Log("DEAD!");
        }
    }

    public void Heal(int amount) {
        if (amount > 0) {
            hp += amount;
            if (hp > maxHp) {
                hp = maxHp;
            }
        }
    }
}
