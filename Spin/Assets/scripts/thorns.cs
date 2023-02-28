using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thorns : MonoBehaviour
{
    public int damage;
    public int del;
    int dell;
    public GameObject blood;
    public bool bleed;
    public int life;
    public GameObject thornDest;
    // Start is called before the first frame update
    void Start()
    {
        dell = del;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dell < del) { dell += 1; }
        life++;
        if (life>400) {
            Instantiate(thornDest, transform.position, transform.rotation);
            Destroy(gameObject); }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (dell == del/*&&!col.gameObject.GetComponent<xq>().player    .GetComponent<Player>().choose*/)
        {
            if (col.gameObject.GetComponent<Player>().player != gameObject.layer - 7)
            {
                col.gameObject.GetComponent<Player>().HB.GetComponent<xq>().HP -= damage; ;
                if (bleed) { Instantiate(blood, transform.position, transform.rotation); }
                dell = 0;
            }
        }
        if (col.gameObject.GetComponent<Player>().player != gameObject.layer - 7) { Destroy(gameObject); }
    }
}
