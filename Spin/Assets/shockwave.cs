using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockwave : MonoBehaviour
{
    public GameObject shock;
    int life;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (life==35)
        {
            shock.SetActive(true);
        }
        if (life>35)
        {
            transform.localScale += new Vector3(0.015f, 0.015f, 0);
        }
        life++;
        if (life>350) { Destroy(gameObject); }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Respawn")
        {
            xq xq = col.gameObject.GetComponent<xq>();
            xq.HP -= 10;
            if (xq.HP <= 0)
            {
                if (Cameraa.track) { Cameraa.slow = 120; Time.timeScale = 0.4f; }
            }
        }
    }
}
