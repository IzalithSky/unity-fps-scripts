using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiController : MonoBehaviour
{
    public TMP_Text hpText;
    public Damageable damagable;

    // Start is called before the first frame update
    void Start()
    {
        hpText.text = "0";    
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = damagable.GetHp().ToString();
    }
}
