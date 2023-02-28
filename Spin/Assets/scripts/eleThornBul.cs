using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eleThornBul : MonoBehaviour
{
    public GameObject thorns;
    public int playLay;
    int del; int life;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * 9;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        life++;
        if (del<1) {
            GameObject Thorns = Instantiate(thorns, transform.position, transform.rotation);
            del = 5; Thorns.layer = playLay+7;
        }
        if (life==-1) { Destroy(gameObject); }
        del--;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (life > 10)
        {
            life = -60;
        }
    }
}
