using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xq : MonoBehaviour
{
    public float HP;
    public int regDel;
    public GameObject player;
    public int del;
    bool spawn;
    public bool urg;
    int heal;
    public GameObject death;
    public GameObject score;
    public GameObject burn;
    public int dmg;
    public bool fullHouse;
    public SpriteRenderer[] classes;
    public Color lost;
    bool inherit;

    Player playScr;
    public int role;
    public GameObject stoneSkin;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        spawn = false;
        playScr = player.GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!urg&&Input.GetKey(KeyCode.Tab))
        {
            if (playScr.player == 3 && Input.GetKeyDown(KeyCode.Alpha3)) { if (!spawn) { spawn = true; player.transform.position = new Vector3(Random.Range(-1.3f, 2.8f), -4f, 0); } }
            if (playScr.player == 4 && Input.GetKeyDown(KeyCode.Alpha4)) { if (!spawn) { spawn = true; player.transform.position = new Vector3(Random.Range(-1.3f, 2.8f), -4f, 0); } }
        }
    }
    void FixedUpdate()
    {
        if (inherit) { playScr.role=0;inherit = false; }
        if (role==0&&!inherit) { role = playScr.role; if (role == 8) { regDel = 250; } }
        if (role==8) { if (regDel > 0) { regDel--;} if (regDel == 1) { stoneSkin.SetActive(true); }
            if (regDel==249) { stoneSkin.SetActive(false); }}
        if (del > 0) { del -= 1; }
        //if (HP < 3) { dmg = 0; }
        if (dmg>0) { dmg -= 1; HP -= 0.1f; }
        if (dmg==1) { burn.SetActive(false); } else if (dmg>40) { burn.SetActive(true); }
        if (urg) { heal += 1; }
        if (heal>24) { heal = 0;HP+=1; }
        transform.position = player.transform.position;
        if (del == 49)
        {
            del = 0; HP = 70; player.transform.position += new Vector3(0, 0, -10);
            playScr.cdr = 0;playScr.cdel = 100;
        }        if (role == 2)
        {
            if (HP < 20) { if (regDel > 0) { regDel -= 1; } }
            if (regDel < 1) { regDel = 50; HP += 1; }
        } 
        if (HP<=0) {
            if (fullHouse)
            {
                classes[role].color = lost; fullHouseMan.fullHouseStc.listVisible(playScr.player-1 ,true);
                Debug.Log("playScr.player: "+playScr.player);
                fullHouseMan.fullHouseStc.lost(playScr.player-1,role);
            }
            role = 0; inherit = true;
            del = 50;
            playScr.unload = false;
            dmg = 0;
            playScr.cd = 10;
            burn.SetActive(false);
            stoneSkin.SetActive(false);
            score.GetComponent<numbers>().convO -= 1;
            Player plyScr = playScr;
            plyScr.axe.SetActive(false);
            plyScr.shield.SetActive(false);
            plyScr.sword.SetActive(false);
            plyScr.dagger.SetActive(false);
            plyScr.bow.SetActive(false);
            plyScr.pole.SetActive(false);
            plyScr.hand.SetActive(false);
            plyScr.arc.SetActive(false);
            plyScr.scy.SetActive(false);
            plyScr.staff.SetActive(false);
            plyScr.eleSword.SetActive(false);
            plyScr.machete.SetActive(false);
            plyScr.sorcSword.SetActive(false);
            plyScr.kunai.SetActive(false);
            plyScr.WSword.SetActive(false);
            plyScr.floatingSword.SetActive(false);
            plyScr.phamSword.SetActive(false);
            plyScr.qwonPos[0].position = plyScr.qwonPos[1].position;
            plyScr.qwonPos[0].rotation = plyScr.qwonPos[1].rotation;
            plyScr.qwonSlash.localScale = Vector3.zero;
            plyScr.qwonSprRend.sprite = plyScr.qwonSpr[0];
            plyScr.qwonSword.SetActive(false);
            plyScr.speedBoost.SetActive(false);
            plyScr.comDel = 0; plyScr.phamSwoSpr.sprite = plyScr.pham[0];
            plyScr.slow = 0;
            plyScr.slowFX.SetActive(false);
            Destroy(plyScr.scy.GetComponent<Rigidbody2D>());
            plyScr.scy.transform.position = player.transform.position; plyScr.scy.transform.parent = player.transform;
            if (plyScr.ffa) { Instantiate(death, transform.position + new Vector3(0, 0, 0), transform.rotation); }
        }
    }
}
