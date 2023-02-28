using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPScaler : MonoBehaviour
{
    public bool FX;
    Vector3 temp;
    float slowDmg;
    public GameObject HitB;
    float x;
    float HP;
    // Start is called before the first frame update
    void Start()
    {
        x = 50;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HP = HitB.GetComponent<xq>().HP;
        if (HP>x) { x = HP; }
        if (HP<=0) { x = 50; }
        temp = transform.localScale;
        if (!FX) { temp.x = HP / x; } else
        {
            if (HP<slowDmg) { slowDmg -= 0.3f; } else if (HP>slowDmg) {slowDmg = HP;}
            temp.x = slowDmg / x;
        }
        transform.localScale = temp;
    }
}
