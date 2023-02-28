using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public int player;
    public float spd;
    public float defspd;
    public int dir;
    public int del;
    public static Player player1Stc;
    public static Player player2Stc;

    public int role;
    public float cd;
    int cdReq;
    int castCD;
    public GameObject abilInd;
    public float cdr;
    public float spinM;
    float ability;
    public bool choose;
    public GameObject axe;
    public GameObject sword;
    public GameObject shield;
    public GameObject dagger;
    public GameObject bow;
    public GameObject pole;
    public GameObject hand;
    public GameObject arc;
    public GameObject scy;
    public GameObject staff;
    public GameObject eleSword; public Sprite[] element; SpriteRenderer eleSwoSpr;
    public GameObject machete;
    public GameObject sorcSword; public Sprite[] sorcSwords; public GameObject sorcHitbox;
    public GameObject kunai;
    public GameObject WSword;
    public GameObject phamSword; public Sprite[] pham; public SpriteRenderer phamSwoSpr;
    public GameObject qwonSword; public Transform[] qwonPos; //0: sword itself 1: default pos 2: defend pos  
    public SpriteRenderer qwonSprRend;  public Sprite[] qwonSpr; public Transform qwonSlash;
    public GameObject qwonX; public int qwXTmr;

    public bool cleave;
    public Transform target;
    public GameObject arrow;
    public GameObject bolt;
    public GameObject sorcBolt;
    public GameObject trap;
    public GameObject det;
    public GameObject lightning;
    public GameObject lightningProj;
    public int Kills;
    public GameObject smoke;
    public GameObject speedBoost;
    int speed;
    public GameObject thornBul;
    public Transform instPos;
    public bool unload;
    bool staffPos;
    public bool isFire;
    Vector3 still;
    public CircleCollider2D[] shadeCol;
    public Vector2[] recall;
    SpriteRenderer sprRend;
    public GameObject bomb;
    public GameObject floatingSword;

    public GameObject HB;
    public int cdel;
    public GameObject CSelect;
    public int roleS; //champ ur selecting, doubles as monk meditate
    int rot;
    int water;
    bool noF;
    public int pre;
    public int comDel; //warrior engage and sorc sword condition and pham charges and qwon bladesurge
    public GameObject comInd;
    public GameObject dect;
    public int att; //reaper scythe and sorc unload and pham discharge and qwon ability 
    public int mlt; //qwon speed decay timer and bomber dashTmr

    public int max;
    public int slow;
    public GameObject slowFX;
    public int stun;

    public bool ffa;
    float mm= 2121 / 1000;
    int fifthSec;
    public bool isDummy;
    public bool cast;

    bool every2;
    Transform trfm;

    private void Awake()
    {
        if (player == 1)
        {
            player1Stc = GetComponent<Player>();
        }
        if (player == 2)
        {
            player2Stc = GetComponent<Player>();
        }
    }

    void Start()
    {
        trfm = transform;
        target = null;
        sprRend = GetComponent<SpriteRenderer>();
        phamSwoSpr = phamSword.GetComponent<SpriteRenderer>();
        eleSwoSpr = eleSword.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        choose = true;
        roleS = 1;
    }

    // Update is called once per frame
    void everyTwo()
    {
        if (qwXTmr>0) { qwXTmr--;if (qwXTmr == 0) { qwonX.SetActive(false); } }
    }
    private void Update()
    {
        if (player == 1)
        {
            if (choose)
            {
                if (rot == 0)
                {
                    if (Input.GetKey(KeyCode.A)) { rot = 6; roleS += 1; }
                    if (Input.GetKey(KeyCode.D)) { rot = -6; roleS -= 1; }
                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
                    {
                        role = roleS; choose = false;
                        if (TourMan.fullHouseStc)
                        {
                            fullHouseMan.fullHouseStc.listVisible(0,false);
                        }
                    }
                    wheel();
                }
                roleV();
            }
            /*if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {
                abilities();
            }*/
            if (Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.LeftAlt)||Input.GetKey(KeyCode.Space))
            {
                abilities();
            }
        }
        if (player == 2)
        {
            if (choose)
            {
                if (rot == 0)
                {
                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.K)) { rot = 6; roleS += 1; }
                    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Semicolon)) { rot = -6; roleS -= 1; }
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        role = roleS; choose = false;
                        if (TourMan.fullHouseStc)
                        {
                            fullHouseMan.fullHouseStc.listVisible(1, false);
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.L))
                    {
                        role = roleS; choose = false;
                        if (TourMan.fullHouseStc)
                        {
                            fullHouseMan.fullHouseStc.listVisible(1, false);
                        }
                    }
                    wheel();
                }
                roleV();
            }
            if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                //abilities();
            }
            /*if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.L))
            {
                abilities();
            }*/
            if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.RightControl))
            {
                abilities();
            }
            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.RightAlt))
            {
                abilities();
            }
            if (Input.GetKey(KeyCode.Question) || Input.GetKey(KeyCode.Backslash) || Input.GetKey(KeyCode.Keypad0)) 
            {
                abilities();
            }
            if (cast) { abilities();cast = false; }
        }
        if (player == 3)
        {
            if (choose)
            {
                if (rot == 0)
                {
                    if (Input.GetKey(KeyCode.F)) { rot = 5; roleS += 1; }
                    if (Input.GetKey(KeyCode.H)) { rot = -5; roleS -= 1; }
                    if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.T)) { role = roleS; choose = false; }
                    wheel();
                }
                roleV();
            }
            if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.G))
            {
                abilities();
            }
        }
        if (player == 4)
        {
            if (choose)
            {
                if (rot == 0)
                {
                    if (Input.GetKey(KeyCode.J)) { rot = 6; roleS += 1; }
                    if (Input.GetKey(KeyCode.L)) { rot = -6; roleS -= 1; }
                    if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.I)) { role = roleS; choose = false; }
                    wheel();
                }
                roleV();
            }
            if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.K))
            {
                abilities();
            }
        }
    }
    void wheel()
    {
        if (roleS < 1) { roleS = 16; }
        if (roleS > 16) { roleS = 1; }
    }
    public void abilities()
    {
        if (stun < 1)
        {
            if (cd <= cdReq && castCD <= 0)
            {
                if (role == 1) { ability = 27; cd = 250; }
                if (role == 2) { ability = 150; if (HB.GetComponent<xq>().HP < max) { ability += (max - HB.GetComponent<xq>().HP) * 1.43f; }; cd = 450; }
                if (role == 3 && target) { if (Vector3.Distance(target.position, transform.position) < 4) { ability = 1; cd = 350; } }
                if (role == 4) { ability = 1; cd = 25; }
                if (role == 5) { ability = 28; cd = 200; }
                if (role == 6) { ability = 1; cd = 150; }
                if (role == 7) { ability = 1; cd = 20; }
                if (role == 8)
                {
                    cd = 100; if (staffPos) { staff.transform.localPosition = new Vector2(0, 0); staffPos = false; }
                    else { staff.transform.localPosition = new Vector2(0, -2); staffPos = true; }
                }
                if (role == 9) { ability = 1; cd = 400; }
                if (role == 10) { ability = 5; cd = 250; }
                if (role == 12) { ability = 25; cd = 50; shadeCol[0].enabled = false; shadeCol[1].enabled = false; sprRend.color = new Color(sprRend.color.r, sprRend.color.g, sprRend.color.b, .5f); }
                if (role == 13)
                {
                    GameObject Bomb = Instantiate(bomb, trfm.position, trfm.rotation);
                    Bomb.layer = player + 7; cd += 50; castCD += 10;
                    noF = true;
                    rb.velocity = trfm.right * 10;
                    mlt = 5;
                }
                if (role == 14) { cd = 50; floatingSword.GetComponent<floatingSword>().release = true; }
                abilInd.SetActive(false);
                if (role == 15)
                {
                    if (comDel < 3) { comDel = 3; phamSwoSpr.sprite = pham[3]; att = 250; }
                    else
                    {
                        comDel = 0; phamSwoSpr.sprite = pham[0]; Instantiate(lightningProj, trfm.position, trfm.rotation); att = 150;
                    }
                    cd = 350;
                }
                if (role == 16)
                {
                    if (att == 0)
                    {
                        qwonSlash.localScale = new Vector3(0f, 1.25f, 1);
                        qwonPos[0].position = qwonPos[2].position;
                        qwonPos[0].rotation = qwonPos[2].rotation;
                        spinM = .2f;
                    }
                    att = 3;
                }
            }
            if (role == 7&&att<1)
            {
                if (Mathf.Abs(scy.transform.position.x - trfm.position.x) < .15f &&
              Mathf.Abs(scy.transform.position.y - trfm.position.y) < .15f)
                {
                    if (scy.transform.parent == null)
                    {
                        Destroy(scy.GetComponent<Rigidbody2D>()); defspd = 1.2f; spd = 1.2f; att = 25;
                        scy.transform.position = trfm.position; scy.transform.parent = trfm;
                        return;
                    }
                }
            }
            if (role == 11 && cd < 300) { unload = true; spinM = 0; if (speed < 1) { spd = spd * 1.25f; } speed = 50; speedBoost.SetActive(true); }
        }
    }
    void roleV()
    {
        cdReq = 0;
        if (role == 1) { axe.SetActive(true); spinM = 1; defspd = 1.2f; }
        if (role == 2) { sword.SetActive(true); shield.SetActive(true); spinM = 1; defspd = 1.2f; }
        if (role == 3) { dagger.SetActive(true); spinM = 1.5f; defspd = 1.44f; target = null; }
        if (role == 4) { bow.SetActive(true); spinM = 0.66f; defspd = 1.38f; }
        if (role == 5) { pole.SetActive(true); hand.SetActive(true); spinM = 1; defspd = 1.2f; }
        if (role == 6) { arc.SetActive(true); spinM = 1; defspd = 1.2f; }
        if (role == 7) { scy.SetActive(true); spinM = 1; defspd = 1.2f; }
        if (role == 8) { staff.SetActive(true); spinM = .5f; defspd = 1.2f; }
        if (role == 9) { eleSword.SetActive(true); spinM = 1f; defspd = 1.2f; }
        if (role == 10) { machete.SetActive(true); spinM = 1f; defspd = 1.2f; }
        if (role == 11) { sorcSword.SetActive(true); spinM = 1f; defspd = 1.2f; comDel = 7; sorcSword.GetComponent<SpriteRenderer>().sprite = sorcSwords[comDel]; }
        if (role == 12) { kunai.SetActive(true); spinM = 1.4f; defspd = 1.38f; }
        if (role == 13) { shield.SetActive(true); spinM = 1f; defspd = 1.2f; cdReq = 100; }
        if (role == 14) { WSword.SetActive(false); spinM = 1f; defspd = 1.32f; floatingSword.SetActive(true); floatingSword.transform.parent = null;
            floatingSword.transform.position = trfm.position;
        }
        if (role==15) { phamSword.SetActive(true); spinM = 1f; defspd = 1.2f; }
        if (role == 16) { qwonSword.SetActive(true); spinM = 1f; defspd = 1.2f; }
        shadeCol[0].enabled = true; shadeCol[1].enabled = true;
        sprRend.color = new Color(sprRend.color.r, sprRend.color.g, sprRend.color.b, 1f);
        spd = defspd;
        slow = 0;
    }
    void sorcReload1()
    {
        if (comDel < 7) { comDel++; }
        sorcSword.GetComponent<SpriteRenderer>().sprite = sorcSwords[comDel];
        sorcSwordAdj();
    }
    void sorcSwordAdj()
    {
        //sorcHitbox.transform.localScale = new Vector3(1,comDel/7f,1);
        sorcSword.GetComponent<CapsuleCollider2D>().offset = new Vector2(0,1.3f+(.3f*(comDel-7)));
        sorcSword.GetComponent<CapsuleCollider2D>().size = new Vector2(1,comDel*4.5f/7f);
    }
    void FixedUpdate()
    {
        if (every2) { every2 = false; } else { every2 = true; everyTwo(); }
        //if (cleave) { GetComponent<Rigidbody2D>().angularDrag = 2f; }
        cleave = false;

        if (role == 13)
        {
            if (mlt==1)
            {
                noF = false;
            }
        }
        if (role==16)
        {
            if (mlt == 1)
            {
                if (spd > 1.2f) { spd = 1.2f; }
            }
            if (att == 1)
            {
                qwonPos[0].position = qwonPos[1].position;
                qwonPos[0].rotation = qwonPos[1].rotation;
                spinM = 1f;
                if (comDel==16)
                {
                    //if (spd <1.56) { spd += .18f; } else { spd = 1.74f; }
                    comDel = 15;
                } else
                {
                    cd = 150;
                }
            }
            if (comDel>0&&att<1)
            {
                if (dir > 0) { qwonSlash.localScale = new Vector3(1.25f, 1.25f, 1); }
                else { qwonSlash.localScale = new Vector3(-1.25f, 1.25f, 1); }
                cleave = true; Debug.Log(cleave);
                comDel--;
                trfm.Rotate(Vector3.forward * 24 * dir);
                if (comDel==0) { cleave = false; qwonSlash.localScale = Vector3.zero; qwonSprRend.sprite = qwonSpr[0]; }
            }
        }
        if (mlt > 0) { mlt--; }
        if (role==15&&comDel>0)
        {
            //if (att<1) { att = 50;comDel--; phamSwoSpr.sprite = pham[comDel]; }
        }
        if (role==3&&target)
        {
            if (Vector3.Distance(target.position, trfm.position) < 4) { dect.SetActive(true); } else
            { dect.SetActive(false); }
        }
        if (fifthSec==0) { fifthSec = 20;
            recall[0] = recall[1];
            recall[1] = recall[2];
            recall[2] = recall[3];
            recall[3] = recall[4];
            recall[4] = trfm.position;
        }
        fifthSec -= 1;
        if (speed>0) { speed--; } if (speed==1) { spd = defspd; speedBoost.SetActive(false); }
        if (unload) {
            if (cd < 301)
            {
                speed = 50;
                if (att < 1)
                {
                    att = 10;
                    GameObject Bolt = Instantiate(sorcBolt, bow.GetComponent<weapon>().blade.transform.position, trfm.rotation);
                    cd += 50;
                    comDel--; sorcSword.GetComponent<SpriteRenderer>().sprite = sorcSwords[comDel];
                    sorcSwordAdj();
                } else if (att==5) { GameObject Bolt = Instantiate(sorcBolt, bow.GetComponent<weapon>().blade.transform.position, trfm.rotation); }
            }
            else
            {
                unload = false; spinM = 1f;
            }
        }
        else if (role==11)
        {
            if (cd==300) { sorcReload1(); }
            if (cd == 250) { sorcReload1(); }
            if (cd == 200) { sorcReload1(); }
            if (cd == 150) { sorcReload1(); }
            if (cd == 100) { sorcReload1(); }
            if (cd == 50) { sorcReload1(); }
            if (cd == 1) { sorcReload1(); }
        }
        //del++;
        if (role==9)
        {
            if (trfm.rotation.eulerAngles.z>0&& trfm.rotation.eulerAngles.z<180) { eleSwoSpr.sprite=element[0]; isFire = true; }
            if (trfm.rotation.eulerAngles.z > 180 && trfm.rotation.eulerAngles.z < 360) { eleSwoSpr.sprite = element[1]; isFire = false; }
        }
        if (role == 8)
        {
            if (trfm.position == still) { comDel++; } else { comDel = 0; }
            still = trfm.position;
            if (comDel>150) { comDel = 0;HB.GetComponent<xq>().HP += 3; }
        }
        if (slow == 1) { spd = defspd; slowFX.SetActive(false); }
        if (slow > 0) { slow -= 1;if (slow>2) { slowFX.SetActive(true); } }
        if (comDel > 0&&role==5) { comDel -= 1; comInd.SetActive(false); } else { comInd.SetActive(true); }
        if (rot > 0) { CSelect.transform.Rotate(Vector3.forward * -3.75f); rot -= 1; }
        if (rot < 0) { CSelect.transform.Rotate(Vector3.forward * 3.75f); rot += 1; }
        if (cdel > 0) { cdel -= 1; }
        if (cdel == 1)
        {
            choose = true; if (ffa) { trfm.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-3, -6), 0); }
            CSelect.SetActive(true); role = 0;
        }
        if (!choose)
        {
            if (stun < 1)
            {
                CSelect.SetActive(false); trfm.Rotate(Vector3.forward * 6 * dir * spinM);
                if (role == 2 && HB.GetComponent<xq>().HP < max) { trfm.Rotate(Vector3.forward * dir * (max - HB.GetComponent<xq>().HP) / 16.7f); }
            } else
            {
                stun--;
            }
        } else
        {
            Vector3 rotty = trfm.rotation.eulerAngles;
            rotty.z = 0.0f;
            trfm.rotation = Quaternion.Euler(rotty);
            HB.GetComponent<xq>().HP = 70;
        }
        if (!noF&&!isDummy)
        {
            float yaxis = Input.GetAxisRaw("Vertical") * 3;
            float xaxis = Input.GetAxisRaw("Horizontal") * 3;
            Vector2 VelocityY = rb.velocity;
            VelocityY.y = yaxis;
            rb.velocity = VelocityY;
            Vector2 VelocityX = rb.velocity;
            VelocityX.x = xaxis;
            rb.velocity = VelocityX;
        }
        if (!noF && !choose && cdel < 1)
        {
            if (player == 1)
            {
                if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.A)) { rb.velocity = new Vector2(-mm * spd, mm * spd); }
                    if (Input.GetKey(KeyCode.D)) { rb.velocity = new Vector2(mm * spd, mm * spd); }
                    if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) { rb.velocity = new Vector2(0, 3 * spd); }
                }
                if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
                {
                    if (Input.GetKey(KeyCode.A)) { rb.velocity = new Vector2(-mm * spd, -mm * spd); }
                    if (Input.GetKey(KeyCode.D)) { rb.velocity = new Vector2(mm * spd, -mm * spd); }
                    if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) { rb.velocity = new Vector2(0, -3 * spd); }
                }
                if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
                {
                    if (Input.GetKey(KeyCode.A)) { rb.velocity = new Vector2(-3 * spd, 0); }
                    if (Input.GetKey(KeyCode.D)) { rb.velocity = new Vector2(3 * spd, 0); }
                }
            }
            if (player == 2)
            {
                if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
                {
                    if (Input.GetKey(KeyCode.LeftArrow)) { rb.velocity = new Vector2(-mm * spd, mm * spd); }
                    if (Input.GetKey(KeyCode.RightArrow)) { rb.velocity = new Vector2(mm * spd, mm * spd); }
                    if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) { rb.velocity = new Vector2(0, 3 * spd); }
                }
                if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
                {
                    if (Input.GetKey(KeyCode.LeftArrow)) { rb.velocity = new Vector2(-mm * spd, -mm * spd); }
                    if (Input.GetKey(KeyCode.RightArrow)) { rb.velocity = new Vector2(mm * spd, -mm * spd); }
                    if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) { rb.velocity = new Vector2(0, -3 * spd); }
                }
                if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
                {
                    if (Input.GetKey(KeyCode.LeftArrow)) { rb.velocity = new Vector2(-3 * spd, 0); }
                    if (Input.GetKey(KeyCode.RightArrow)) { rb.velocity = new Vector2(3 * spd, 0); }
                }



                if (Input.GetKey(KeyCode.O) && !Input.GetKey(KeyCode.DownArrow))
                {
                    if (Input.GetKey(KeyCode.K)) { rb.velocity = new Vector2(-mm * spd, mm * spd); }
                    if (Input.GetKey(KeyCode.Semicolon)) { rb.velocity = new Vector2(mm * spd, mm * spd); }
                    if (!Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.Semicolon)) { rb.velocity = new Vector2(0, 3 * spd); }
                }
                if (Input.GetKey(KeyCode.L))
                {
                    if (Input.GetKey(KeyCode.K)) { rb.velocity = new Vector2(-mm * spd, -mm * spd); }
                    if (Input.GetKey(KeyCode.Semicolon)) { rb.velocity = new Vector2(mm * spd, -mm * spd); }
                    if (!Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.Semicolon)) { rb.velocity = new Vector2(0, -3 * spd); }
                }
                if (!Input.GetKey(KeyCode.O) && !Input.GetKey(KeyCode.L))
                {
                    if (Input.GetKey(KeyCode.K)) { rb.velocity = new Vector2(-3 * spd, 0); }
                    if (Input.GetKey(KeyCode.Semicolon)) { rb.velocity = new Vector2(3 * spd, 0); }
                }
            }
            if (player == 3)
            {
                if (Input.GetKey(KeyCode.T) && !Input.GetKey(KeyCode.G))
                {
                    if (Input.GetKey(KeyCode.F)) { rb.velocity = new Vector2(-mm * spd, mm * spd); }
                    if (Input.GetKey(KeyCode.H)) { rb.velocity = new Vector2(mm * spd, mm * spd); }
                    if (!Input.GetKey(KeyCode.F) && !Input.GetKey(KeyCode.H)) { rb.velocity = new Vector2(0, 3 * spd); }
                }
                if (Input.GetKey(KeyCode.G) && !Input.GetKey(KeyCode.T))
                {
                    if (Input.GetKey(KeyCode.F)) { rb.velocity = new Vector2(-mm * spd, -mm * spd); }
                    if (Input.GetKey(KeyCode.H)) { rb.velocity = new Vector2(mm * spd, -mm * spd); }
                    if (!Input.GetKey(KeyCode.F) && !Input.GetKey(KeyCode.H)) { rb.velocity = new Vector2(0, -3 * spd); }
                }
                if (!Input.GetKey(KeyCode.T) && !Input.GetKey(KeyCode.G))
                {
                    if (Input.GetKey(KeyCode.F)) { rb.velocity = new Vector2(-3 * spd, 0); }
                    if (Input.GetKey(KeyCode.H)) { rb.velocity = new Vector2(3 * spd, 0); }
                }
            }
            if (player == 4)
            {
                if (Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K))
                {
                    if (Input.GetKey(KeyCode.Alpha1)) { rb.velocity = new Vector2(-mm * spd, mm * spd); }
                    if (Input.GetKey(KeyCode.Alpha3)) { rb.velocity = new Vector2(mm * spd, mm * spd); }
                    if (!Input.GetKey(KeyCode.Alpha1) && !Input.GetKey(KeyCode.H)) { rb.velocity = new Vector2(0, 3 * spd); }
                }
                if (Input.GetKey(KeyCode.Alpha2) && !Input.GetKey(KeyCode.Alpha5))
                {
                    if (Input.GetKey(KeyCode.Alpha1)) { rb.velocity = new Vector2(-mm * spd, -mm * spd); }
                    if (Input.GetKey(KeyCode.Alpha3)) { rb.velocity = new Vector2(mm * spd, -mm * spd); }
                    if (!Input.GetKey(KeyCode.Alpha1) && !Input.GetKey(KeyCode.H)) { rb.velocity = new Vector2(0, -3 * spd); }
                }
                if (!Input.GetKey(KeyCode.Alpha5) && !Input.GetKey(KeyCode.Alpha2))
                {
                    if (Input.GetKey(KeyCode.Alpha1)) { rb.velocity = new Vector2(-3 * spd, 0); }
                    if (Input.GetKey(KeyCode.Alpha3)) { rb.velocity = new Vector2(3 * spd, 0); }
                }
            }
        }
        if (cd > 0 && !unload) { cd -= 1 + cdr; }
        if (castCD>0) { castCD--; }
        if (cd<=cdReq) { abilInd.SetActive(true); }
        if (role == 4) { spinM = .66F; }
        if (ability > 0)
        {
            if (role == 1)
            {
                if (ability > 14) { trfm.Rotate(Vector3.forward * -33 * dir); }
                trfm.Rotate(Vector3.forward * 24 * dir); cleave = true;
            }
            if (role == 2)
            {
                cleave = true; GetComponent<Rigidbody2D>().angularDrag = 500;
            }
            if (role == 3) {
                float oldRot = trfm.eulerAngles.z;
                Vector2 direction = target.position - trfm.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                trfm.rotation = Quaternion.Slerp(trfm.rotation, rotation, 100 * Time.deltaTime);
                trfm.position += trfm.up * Vector3.Distance(trfm.position,target.position);
                trfm.position += trfm.up * 1;
                trfm.eulerAngles = new Vector3(trfm.eulerAngles.x, trfm.eulerAngles.y, oldRot);
                Instantiate(smoke, trfm.position, trfm.rotation); }
            if (role == 4)
            {
                if (ability == 1)
                {
                    GameObject Arrow = Instantiate(arrow, bow.GetComponent<weapon>().blade.transform.position, trfm.rotation);
                    Arrow.GetComponent<arrow>().bow = bow; Arrow.GetComponent<arrow>().player = gameObject;
                    Arrow.GetComponent<arrow>().HitB = HB;
                } //spinM = 0;
            }
            if (role == 5)
            {
                if (ability > 22) { noF = true; trfm.Rotate(Vector3.forward * 6 * -dir * spinM); rb.velocity = trfm.up * 20; }
                HB.SetActive(false);
                if (ability == 21 && comDel > 0) { ability = 0; noF = false; return; }
                if (ability < 22 && ability > 2) { noF = false; trfm.Rotate(Vector3.forward * 14 * dir); cleave = true; }
            }
            if (role == 6)
            {
                GameObject Bolt = Instantiate(bolt, arc.GetComponent<weapon>().blade.transform.position, trfm.rotation);
                GameObject Det = Instantiate(det, trfm.position, trfm.rotation);
                Det.GetComponent<Explode>().player = gameObject;
                Det.GetComponent<Explode>().arc = arc;
                Det.GetComponent<Explode>().hb = HB;
            }
            if (role == 7)
            {
                if (scy.transform.parent==trfm&&att<1) { scy.transform.parent = null; att = 25; defspd = 1.68f; if (slow < 1) { spd = 1.68f; }
                    Physics2D.IgnoreCollision(scy.GetComponent<CapsuleCollider2D>(), GetComponent<CircleCollider2D>());
                }
                else if (att<1)
                {
                    Destroy(scy.GetComponent<Rigidbody2D>());
                    Vector2 direction = trfm.position - scy.transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                    scy.transform.rotation = Quaternion.Slerp(trfm.rotation, rotation, 50 * Time.deltaTime);
                    scy.AddComponent<Rigidbody2D>().gravityScale = 0; scy.GetComponent<Rigidbody2D>().collisionDetectionMode=CollisionDetectionMode2D.Continuous;
                    scy.GetComponent<Rigidbody2D>().velocity = scy.transform.up * 12;
                }
            }
            if (role==8&&ability>1) { spd = 1.56f; } else if (role==8){spd=1.2f; speedBoost.SetActive(false); }
            if (role == 9)
            {
                GameObject Thorn = Instantiate(thornBul, arc.GetComponent<weapon>().blade.transform.position, trfm.rotation);
                Thorn.GetComponent<eleThornBul>().playLay = player;
            }
            if (role==10)
            {
                /*if (ability==5) {GameObject Trap=Instantiate(trap, instPos.position, transform.rotation); Trap.GetComponent<trap>().player = gameObject;
                    Trap.GetComponent<trap>().weapon = machete; Trap.layer = player + 7;
                }
                if (ability == 3) { GameObject Trap = Instantiate(trap, instPos.position, transform.rotation); Trap.GetComponent<trap>().player = gameObject;
                    Trap.GetComponent<trap>().weapon = machete; Trap.layer = player + 7;
                }
                if (ability == 1) { GameObject Trap = Instantiate(trap, instPos.position, transform.rotation); Trap.GetComponent<trap>().player = gameObject;
                    Trap.GetComponent<trap>().weapon = machete; Trap.layer = player + 7;
                }*/
                GameObject Trap = Instantiate(trap, instPos.position, trfm.rotation); Trap.GetComponent<trap>().player = gameObject;
                if (ability ==1|| ability == 5) { Trap.GetComponent<trap>().super = 1; }
                if (ability == 3) { Trap.GetComponent<trap>().super = -1; }
                Trap.GetComponent<trap>().weapon = machete; Trap.layer = player + 7;
            }
            if (ability==1)
            {
                if (role==12)
                {
                    shadeCol[0].enabled = true; shadeCol[1].enabled = true;
                    sprRend.color = new Color(sprRend.color.r, sprRend.color.g, sprRend.color.b, 1f);
                }
            }
            ability -= 1;
        }
        if (att>0) { att -= 1; }
        else
        {
            Vector3 rotty = trfm.rotation.eulerAngles;
            rotty.y = 0.0f;
            rotty.x = 0.0f;
            trfm.rotation = Quaternion.Euler(rotty);
            HB.transform.position = trfm.position;
            HB.SetActive(true);
        }
    }
}

