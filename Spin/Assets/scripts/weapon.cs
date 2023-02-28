using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public int dmg;
    public Player playScr;
    public GameObject player;
    public GameObject sparks;
    public GameObject blood;
    public Transform blade;
    public GameObject HitB;
    public CapsuleCollider2D col;
    int del;
    int del2;
    int antiCrit;
    public Color gold;
    public Color white;
    public GameObject upg1;
    public GameObject upg2;
    public GameObject upg3;
    int custom; //flurry for monk(8)
    int role;
    int roleCol;
    bool every2;
    public int special; //0:nothing 1:floatingSword
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        playScr = player.GetComponent<Player>();
        if (special == 1) { rb = GetComponent<Rigidbody2D>(); }
        if (dmg==0) { dmg = 10; }
        Physics2D.IgnoreCollision(HitB.GetComponent<CircleCollider2D>(), col);
    }
    void Update()
    {
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (every2)
        {

            if (special == 1) { rb.velocity=transform.up*7f;
                Vector2 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
            }
            every2 = false;
        }
        else {every2 = true;}
        if (custom > 0) { custom--; } if (custom==1) { playScr.spd =1.2f; playScr.spinM =1; }
        //if (Input.GetKey(KeyCode.Space)) { transform.parent = null;transform.Rotate(Vector3.forward*20); }
        if (del > 0) { del -= 1; }
        if (antiCrit>0) { antiCrit--; }
        if (del2==8&&!playScr.cleave) { playScr.dir*= -1; }
        if (del2 > 0) { del2 -= 1; }
        //if (del2==1) { playScr.dir = playScr.dir * -1; }
        if (playScr.cleave && playScr.role == 2) { GetComponent<SpriteRenderer>().color = gold;
            GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
        else if (!playScr.cleave && playScr.role == 2) { GetComponent<SpriteRenderer>().color = white;
            GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (playScr.cleave) {
            xq xqScr = col.gameObject.GetComponent<xq>();
            if (col.gameObject.tag == "Respawn" && !xqScr.player.GetComponent<Player>().choose)
            {
                role = playScr.role;
                if (role == 2 || role == 1)
                {
                    roleCol = xqScr.player.GetComponent<Player>().role;
                    GameObject Blood = Instantiate(blood, blade.position, transform.rotation);
                    xqScr.HP -= dmg;
                    del = 10;
                    if (role == 1)
                    {
                        HitB.GetComponent<xq>().HP += 3;
                        if (playScr.cleave)
                        {
                            if (xqScr.player.GetComponent<Player>().slow < 2) { xqScr.player.GetComponent<Player>().spd = xqScr.player.GetComponent<Player>().spd / 2; }
                            xqScr.player.GetComponent<Player>().slow += 100;
                        }
                    } else
                    {
                        xqScr.player.GetComponent<Player>().slow = 100;
                        xqScr.player.GetComponent<Player>().spd -=.18f;
                    }
                    //if (role==8) { if (player.GetComponent<Player>().spinM < 2) { player.GetComponent<Player>().spd += .072f; player.GetComponent<Player>().spinM += 0.2f; } custom = 151;  } FLURRY PASSIVE
                    if (roleCol == 5) { xqScr.player.GetComponent<Player>().comDel = 200; }
                    if (roleCol == 6) { xqScr.player.GetComponent<Player>().cd -= 200; }
                    if (roleCol==12) { xqScr.player.transform.position = xqScr.player.GetComponent<Player>().recall[0]; Debug.Log("recall");
                        xqScr.player.GetComponent<Player>().cd = 200;
                    }
                    if (roleCol == 7)
                    {
                        xqScr.player.GetComponent<Player>().slow = 100;
                        xqScr.player.GetComponent<Player>().spd = .84f;
                    }
                    if (xqScr.HP <= 0)
                    {
                        if (Cameraa.track) { Cameraa.slow = 120; Time.timeScale = 0.4f; } else { display.show = 50; }
                        int pwrup = Random.Range(1, 4);
                        if (pwrup == 1) { HitB.GetComponent<xq>().HP += 30; Instantiate(upg1, transform.position, transform.rotation); }
                        if (pwrup == 2)
                        {
                            player.GetComponent<Player>().spd += .12f;
                            Instantiate(upg2, transform.position, transform.rotation);
                        }
                        if (pwrup == 3) { player.GetComponent<Player>().cdr += .1f; Instantiate(upg3, transform.position, transform.rotation); }
                        player.GetComponent<Player>().Kills = 1;
                    }
                }
            }
            else { if (del < 1) { GameObject Sparks = Instantiate(sparks, blade.position, transform.rotation); Cameraa.zoom = 3; } }
        }
        if (col.gameObject.tag == "thorns" && col.gameObject.layer != player.GetComponent<Player>().player + 7) {
            col.gameObject.GetComponent<thorns>().life += 350;
            if (del2 < 1 && playScr.cleave == false) { del2 = 8; }
        }
        if (col.gameObject.tag == "trap" && col.gameObject.layer != player.GetComponent<Player>().player + 7)
        {
            col.gameObject.GetComponent<trap>().life = 499;
            if (del2 < 1 && playScr.cleave == false) { del2 = 8; }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //del2 = 5;
        if (del2 < 1 && playScr.cleave == false) {del2 = 8; }
        if (col.gameObject.tag == "Respawn"&&antiCrit<1)
        {
            antiCrit=15;
            xq nmyHB = col.gameObject.GetComponent<xq>();
            Player myPlayer = player.GetComponent<Player>();
            GameObject nmyCol = nmyHB.player;
            Player nmyPlayer = nmyCol.GetComponent<Player>();
            if (nmyCol.GetComponent<Player>().choose) { return; }
            role = player.GetComponent<Player>().role;
            roleCol =nmyPlayer.role;
            GameObject Blood = Instantiate(blood, blade.position, transform.rotation);
            //if (role==12) { player.transform.position = myPlayer.recall[0]; }
            if (role==9) {if (myPlayer.isFire) { nmyHB.dmg = 75; }
            else {
                    if (nmyPlayer.slow < 125) { nmyPlayer.slow = 125; }
                    if (nmyPlayer.spd > nmyPlayer.defspd / 2)
                    {
                        nmyPlayer.spd = nmyPlayer.defspd / 2;
                    }
                }
            }
            if (role==16)
            {
                if (playScr.comDel>0&&playScr.comDel!=16)
                {
                    nmyPlayer.qwonX.SetActive(true); nmyPlayer.qwXTmr = 50;
                } else if (nmyPlayer.qwXTmr>0)
                {
                    nmyPlayer.qwXTmr = 1;
                    float x0 = (70 - HitB.GetComponent<xq>().HP) / 3;
                    if (x0<1) { x0 = 1; }
                    nmyHB.HP -= x0;
                    //  missing hp lifesteal
                    /*float posHp = (70 - HitB.GetComponent<xq>().HP) / 4;
                    Debug.Log(posHp);
                    if (posHp > 0) { if (posHp < 1) { posHp = 1; } HitB.GetComponent<xq>().HP += posHp; }*/
                }
                playScr.spd += .12f; if (playScr.spd>1.68f) { playScr.spd = 1.68f; } playScr.mlt = 150;
            }
            if (roleCol==16)
            {
                //nmyPlayer.spd = nmyPlayer.defspd;
            }
            if (roleCol == 12) { nmyCol.transform.position =nmyPlayer.recall[0];
                nmyPlayer.cd = 200; nmyPlayer.abilInd.SetActive(false);
            }
            if (roleCol!=8) {
                if (role == 7 && myPlayer.scy.transform.parent == null)
                {
                    nmyHB.HP -= 6;
                } else
                if (role!=16||playScr.att==0) { nmyHB.HP -= dmg; }
            } else 
            {
                if (nmyHB.regDel > 1) { nmyHB.HP -= dmg; }
                nmyHB.regDel = 250;
            }
            del = 10;
            if (role == 1) { HitB.GetComponent<xq>().HP += 2;
                if (player.GetComponent<Player>().cleave)
                {
                    if (nmyCol.GetComponent<Player>().slow < 2) {nmyPlayer.spd =nmyPlayer.spd / 2; }
                    nmyPlayer.slow += 100;
                    HitB.GetComponent<xq>().HP += 5;
                }
            }
            if (role==10) { if (nmyPlayer.slow > 1) { nmyHB.HP -= 5; } }
            if (role==3) { myPlayer.cd -= 250; myPlayer.target = nmyCol.transform; }
            //if (role==5) { myPlayer.comDel = 200; myPlayer.cd -= 50; }
            //warrior lose engage on dealing damage ^
            //if (role==8) { if (player.GetComponent<Player>().spinM < 2) { player.GetComponent<Player>().spd += .072f; player.GetComponent<Player>().spinM += 0.2f; } custom = 151;  } FLURRY PASSIVE
            if (roleCol == 5) {nmyPlayer.comDel = 200; }
            if (roleCol == 6) {nmyPlayer.cd -= 200; }
            if (roleCol==7) {nmyPlayer.slow = 125;
               nmyPlayer.spd = .84f;
            }
            if (role == 15)
            {
                if (myPlayer.comDel < 3)
                {
                    //Phamm receives charge when landing hits
                    //myPlayer.comDel += 1; myPlayer.phamSwoSpr.sprite = myPlayer.pham[myPlayer.comDel];
                }
                else
                {
                    myPlayer.comDel = 0; myPlayer.phamSwoSpr.sprite = myPlayer.pham[0]; nmyHB.HP -= 10;
                    nmyPlayer.spd = 0; myPlayer.att = 250;
                    nmyPlayer.stun += 50;
                    nmyPlayer.slow += 50;
                    Instantiate(myPlayer.lightning, col.transform.position, myPlayer.lightning.transform.rotation);
                }
            }
            if (roleCol==15) { if (nmyPlayer.comDel < 3) { nmyPlayer.comDel += 1; nmyPlayer.phamSwoSpr.sprite = nmyPlayer.pham[nmyPlayer.comDel];
                    nmyPlayer.att = 250;
                } }
            if (nmyHB.HP <= 0)            {
                if (Cameraa.track) { Cameraa.slow = 120; Time.timeScale = 0.4f; } else { display.show = 50; }
                if (nmyPlayer.ffa)
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
                }
            }
        }
        else
        {
            if (del < 1) { GameObject Sparks = Instantiate(sparks, blade.position, transform.rotation);Cameraa.zoom = 3; }
            if (playScr.role==16&&playScr.att>0&&col.gameObject.tag=="Player")
            {
                playScr.spd += .24f; if (playScr.spd > 1.68f) { playScr.spd = 1.68f; }
                playScr.qwonSprRend.sprite = playScr.qwonSpr[1];
                playScr.comDel = 16;
            }
        }
    }
}
