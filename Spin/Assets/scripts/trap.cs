using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    private Rigidbody2D rb;
    public int del2;
    public GameObject blood;
    public GameObject sparks;
    int del;
    public CapsuleCollider2D col;
    public GameObject weapon;
    public GameObject player;
    public GameObject upg1;
    public GameObject upg2;
    public GameObject upg3;
    public int life;
    int hold;
    public int super;
    public Color superCol;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (super==1) { GetComponent<SpriteRenderer>().color = superCol; rb.velocity = transform.up * 3.5f; } else
            if (super == 0)
        {
            rb.velocity = transform.up * 3f;
        } else
        {
            rb.velocity = transform.up * 2.5f;
        }
        //Physics2D.IgnoreCollision(HitB.GetComponent<CircleCollider2D>(), col);
        //Physics2D.IgnoreCollision(weapon.GetComponent<CircleCollider2D>(), col);
    }

    // Update is called once per frame
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S)) { hold = 5; } if (hold>0) { hold--; }
        //if (Input.GetKey(KeyCode.W)&&hold>0) { if (player.GetComponent<Player>().cd > 1) { rb.velocity = transform.up * 0; life = -250; } }
    }
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * 1.5f);
        del2 += 1; life++;
        //if (del2 > 10) { col.enabled = true; }
        if (super==1) {
            if (life > 1000) { GameObject Sparks = Instantiate(sparks, transform.position, transform.rotation); Destroy(gameObject); }
        } else
        {
            if (life > 500) { GameObject Sparks = Instantiate(sparks, transform.position, transform.rotation); Destroy(gameObject); }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "thorns")
        {
            col.gameObject.GetComponent<thorns>().life = 499;
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Respawn")
        {
            int role = col.gameObject.GetComponent<xq>().player.GetComponent<Player>().role;
            GameObject Blood = Instantiate(blood, transform.position, transform.rotation);
            col.gameObject.GetComponent<xq>().HP -= 10;/**player.GetComponent<Player>().pre; player.GetComponent<Player>().pre += 1;*/
            del = 10;
            if (col.gameObject.GetComponent<xq>().player.GetComponent<Player>().slow < 2) { col.gameObject.GetComponent<xq>().player.GetComponent<Player>().spd = col.gameObject.GetComponent<xq>().player.GetComponent<Player>().spd / 2; }
            col.gameObject.GetComponent<xq>().player.GetComponent<Player>().slow += 50;
            if (role == 5) { col.gameObject.GetComponent<xq>().player.GetComponent<Player>().comDel = 200; }
            if (role == 6) { col.gameObject.GetComponent<xq>().player.GetComponent<Player>().cd -= 200; }
            if (col.gameObject.GetComponent<xq>().HP <= 0)
            {
                if (Cameraa.track) { Cameraa.slow = 100; Time.timeScale = 0.3f; } else { display.show = 50; }
                int pwrup = Random.Range(1, 4);
                //if (pwrup == 1) { HitB.GetComponent<xq>().HP += 30; Instantiate(upg1, transform.position, transform.rotation); }
                if (pwrup == 2)
                {
                    player.GetComponent<Player>().spd += .12f;
                    Instantiate(upg2, transform.position, transform.rotation);
                }
                if (pwrup == 3) { player.GetComponent<Player>().cdr += .1f; Instantiate(upg3, transform.position, transform.rotation); }
                player.GetComponent<Player>().Kills = 1;
            }
            if (col.gameObject.GetComponent<xq>().player.GetComponent<Player>().slow < 2) { col.gameObject.GetComponent<xq>().player.GetComponent<Player>().spd = col.gameObject.GetComponent<xq>().player.GetComponent<Player>().spd / 4; }
            col.gameObject.GetComponent<xq>().player.GetComponent<Player>().slow += 150;
            Destroy(gameObject);
        }
        else
        {
            if (del < 1)
            {
                //player.GetComponent<Player>().pre = 1;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
       
    }
}
