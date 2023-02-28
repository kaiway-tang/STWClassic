using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingSword : MonoBehaviour
{
    public Transform player;
    Rigidbody2D rb;

    public GameObject blood;
    public GameObject sparks;
    public Transform blade;
    public bool release;
    bool once;

    int del;
    int role;
    int roleCol;
    int antiCrit;
    public GameObject lostSoul;
    public CapsuleCollider2D capsCol;
    int disable;
    public SpriteRenderer sprRend;
    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (antiCrit>0) { antiCrit--; }
        if (del>0) {
            if (del == 6)
            {
                transform.Rotate(Vector3.forward * 180);
            }
            del--;  }
        if (disable>0) { disable--;if (disable == 1) { capsCol.enabled = true; sprRend.color = colors[0]; } }
        if (release)
        {
            /*if (!once) {
                Vector2 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
                transform.Rotate(Vector3.forward * 180);
                once = true;
            }*/
            rb.velocity = transform.up * 32f;
        }
        else
        {
            rb.velocity = transform.up * 9f;
            Vector2 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 3.5f * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (del < 1)
        {
            if (release) { release = false; once = false; }
            if (del < 1) {del = 8; }
            if (col.gameObject.tag == "Respawn"&&disable<1)
            {
                disable = 50;
                capsCol.enabled = false;
                sprRend.color = colors[1];
                xq nmyHB = col.gameObject.GetComponent<xq>();
                Player myPlayer = player.GetComponent<Player>();
                GameObject nmyCol = nmyHB.player;
                Player nmyPlayer = nmyCol.GetComponent<Player>();
                if (nmyCol.GetComponent<Player>().choose) { return; }
                role = player.GetComponent<Player>().role;
                roleCol = nmyPlayer.role;
                GameObject Blood = Instantiate(blood, blade.position, transform.rotation);
                //if (role==12) { player.transform.position = myPlayer.recall[0]; }
                if (role == 9)
                {
                    if (myPlayer.isFire) { nmyHB.dmg = 50; }
                    else
                    {
                        nmyPlayer.slow = 50;
                        nmyPlayer.spd = nmyPlayer.spd / 2;
                    }
                }
                if (roleCol == 12)
                {
                    nmyCol.transform.position = nmyPlayer.recall[0];
                    nmyPlayer.cd = 200; nmyPlayer.abilInd.SetActive(false);
                }
                if (antiCrit < 1)
                {
                    if (roleCol != 8) { doDmg(nmyHB,nmyCol); }
                    else
                    {
                        if (nmyHB.regDel > 1) { doDmg(nmyHB, nmyCol); }
                        nmyHB.regDel = 250;
                    }
                    antiCrit = 15;
                }
                if (role == 10) { if (nmyPlayer.slow > 1) { nmyHB.HP -= 5; } }
                if (role == 3) { myPlayer.cd -= 250; myPlayer.target = nmyCol.transform; }
                if (role == 5) { myPlayer.comDel = 200; myPlayer.cd -= 50; }
                //if (role==8) { if (player.GetComponent<Player>().spinM < 2) { player.GetComponent<Player>().spd += .072f; player.GetComponent<Player>().spinM += 0.2f; } custom = 151;  } FLURRY PASSIVE
                if (roleCol == 5) { nmyPlayer.comDel = 200; }
                if (roleCol == 6) { nmyPlayer.cd -= 200; }
                if (roleCol == 7)
                {
                    nmyPlayer.slow = 75;
                    nmyPlayer.spd = .84f;
                }
                if (nmyHB.HP <= 0)
                {
                    if (Cameraa.track) { Cameraa.slow = 120; Time.timeScale = 0.4f; } else { display.show = 50; }
                    /*if (nmyPlayer.ffa)
                    {
                        int pwrup = Random.Range(1, 4);
                        if (pwrup == 1) { HitB.GetComponent<xq>().HP += 30; Instantiate(upg1, transform.position, transform.rotation); }
                        if (pwrup == 2)
                        {
                            player.GetComponent<Player>().spd += .12f;
                            Instantiate(upg2, transform.position, transform.rotation);
                        }
                        if (pwrup == 3) { player.GetComponent<Player>().cdr += .1f; Instantiate(upg3, transform.position, transform.rotation); }
                        player.GetComponent<Player>().Kills = 1;
                    }*/
                }
            }
            else { GameObject Sparks = Instantiate(sparks, blade.position, transform.rotation); Cameraa.zoom = 3; }
        }
    }
    void doDmg(xq nmyHB, GameObject nmyCol)
    {
        nmyHB.HP -= 16;
        for (int i = 0; i < 2; i++)
        {
            GameObject Lost= Instantiate(lostSoul, nmyCol.transform.position, transform.rotation);
            Lost.GetComponent<lostSoul>().player = player;
        }
    }
    private void OnTriggerEnter2D(Collision2D col)
    {
        //if (del < 1)
        {
            if (release) { release = false; once = false; }
            if (del<1) { del = 8; }
            if (col.gameObject.tag == "Respawn")
            {
                xq nmyHB = col.gameObject.GetComponent<xq>();
                Player myPlayer = player.GetComponent<Player>();
                GameObject nmyCol = nmyHB.player;
                Player nmyPlayer = nmyCol.GetComponent<Player>();
                if (nmyCol.GetComponent<Player>().choose) { return; }
                role = player.GetComponent<Player>().role;
                roleCol = nmyPlayer.role;
                GameObject Blood = Instantiate(blood, blade.position, transform.rotation);
                //if (role==12) { player.transform.position = myPlayer.recall[0]; }
                if (role == 9)
                {
                    if (myPlayer.isFire) { nmyHB.dmg = 50; }
                    else
                    {
                        nmyPlayer.slow = 50;
                        nmyPlayer.spd = nmyPlayer.spd / 2;
                    }
                }
                if (roleCol == 12)
                {
                    nmyCol.transform.position = nmyPlayer.recall[0];
                    nmyPlayer.cd = 200; nmyPlayer.abilInd.SetActive(false);
                }
                if (antiCrit < 1)
                {
                    if (roleCol != 8) { nmyHB.HP -= 10;}
                    else
                    {
                        if (nmyHB.regDel > 1) { nmyHB.HP -= 10; }
                        nmyHB.regDel = 250;
                    }
                    antiCrit = 15;
                }
                if (role == 10) { if (nmyPlayer.slow > 1) { nmyHB.HP -= 5; } }
                if (role == 3) { myPlayer.cd -= 250; myPlayer.target = nmyCol.transform; }
                if (role == 5) { myPlayer.comDel = 200; myPlayer.cd -= 50; }
                //if (role==8) { if (player.GetComponent<Player>().spinM < 2) { player.GetComponent<Player>().spd += .072f; player.GetComponent<Player>().spinM += 0.2f; } custom = 151;  } FLURRY PASSIVE
                if (roleCol == 5) { nmyPlayer.comDel = 200; }
                if (roleCol == 6) { nmyPlayer.cd -= 200; }
                if (roleCol == 7)
                {
                    nmyPlayer.slow = 75;
                    nmyPlayer.spd = .84f;
                }
                if (nmyHB.HP <= 0)
                {
                    if (Cameraa.track) { Cameraa.slow = 120; Time.timeScale = 0.4f; } else { display.show = 50; }
                    /*if (nmyPlayer.ffa)
                    {
                        int pwrup = Random.Range(1, 4);
                        if (pwrup == 1) { HitB.GetComponent<xq>().HP += 30; Instantiate(upg1, transform.position, transform.rotation); }
                        if (pwrup == 2)
                        {
                            player.GetComponent<Player>().spd += .12f;
                            Instantiate(upg2, transform.position, transform.rotation);
                        }
                        if (pwrup == 3) { player.GetComponent<Player>().cdr += .1f; Instantiate(upg3, transform.position, transform.rotation); }
                        player.GetComponent<Player>().Kills = 1;
                    }*/
                }
            }
            else { GameObject Sparks = Instantiate(sparks, blade.position, transform.rotation); Cameraa.zoom = 3; }
        }
    }
}
