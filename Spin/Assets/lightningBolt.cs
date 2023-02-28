using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightningBolt : MonoBehaviour
{
    public SpriteRenderer[] sprRend;
    public Sprite[] bolts;
    public bool dmg;
    int sprNum;
    int last;
    bool every2;
    public int life;
    // Start is called before the first frame update
    void Start()
    {
        last = 4;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (every2)
        {
            sprNum = Random.Range(0, 3);
            if (sprNum == last) { sprNum = Random.Range(0, 3); }
            if (sprNum == last) { sprNum = Random.Range(0, 3); }
            if (sprNum == last) { sprNum = Random.Range(0, 3); }
            sprRend[0].sprite = bolts[sprNum];
            if (dmg) { sprRend[1].sprite = bolts[sprNum]; }
            last = sprNum;
            every2 = false;
            every2 = true;
        }else
        {
            every2 = true;
        }
        life++;
        if (life>4) { Destroy(gameObject); }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (dmg&& col.gameObject.tag == "Respawn")
        {
            xq xq = col.gameObject.GetComponent<xq>();
            Player nmyPlayer = xq.player.GetComponent<Player>();
            nmyPlayer.spd = 0;
            nmyPlayer.stun += 50;
            nmyPlayer.slow += 50;
            xq.HP -= 10;
            if (xq.HP <= 0)
            {
                if (Cameraa.track) { Cameraa.slow = 120; Time.timeScale = 0.4f; }
            }
        }
    }
}
