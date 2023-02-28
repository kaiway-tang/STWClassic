using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    int del;
    public SpriteRenderer sprRend;
    public Color color;
    public GameObject explosion;
    public SpriteRenderer explSpr;
    public Color explColor;
    bool kaboom;
    bool doneDmg;
    public Sprite[] rob;
    float spd;
    // Start is called before the first frame update
    void Start()
    {
        sprRend.sprite = rob[gameObject.layer-8];
        del = 45;
        explosion.layer = gameObject.layer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (del>50)
        {
            color.g -= 0.02f;
            color.b -= 0.02f;
            sprRend.color = color;
            del--;
            if (del==51) { explosion.SetActive(true);del = 105;
                sprRend.sprite = null;
            }
            if (del>101&&del<123) { explSpr.color = explColor;
                explColor.r -= 0.20f;
                explColor.g -= 0.20f;
                explColor.b -= 0.20f;
                explColor.a -= 0.20f;
            } else if (del==101) { Destroy(gameObject); }
        } else if (del<50)
        {
            del++;
        }
        if (kaboom) { transform.position -= transform.up * spd; }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (kaboom&& col.gameObject.name != "explode")
        {
            //if (col.gameObject.name != "bomb(Clone)")
            {
                del = 52;
            }
        }
        if (col.gameObject.name == "explode") { kaboom = true; spd += 0.2f;
            Vector2 direction = col.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
        }
        if (del==50&& col.gameObject.tag == "Player")
        {
            del = 100;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Respawn" && del > 102)
        {
            if (!doneDmg)
            {
                xq xq = col.gameObject.GetComponent<xq>();
                xq.HP -= 10;
                if (xq.HP <= 0)
                {
                    if (Cameraa.track) { Cameraa.slow = 120; Time.timeScale = 0.4f; }
                }
                doneDmg = true;
            }
        }
    }
}
