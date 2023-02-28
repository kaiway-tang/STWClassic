using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : MonoBehaviour
{
    public float spd;
    float defSpd;
    float negSpd;
    public float x;
    public float defDist; //default distance
    public float funcDist; //functional distance
    public Transform player;
    public Player playerScript;

    Rigidbody2D rb;
    int del;
    bool every2;
    float mm = 2121 / 1000;
    public float angle;
    public float nmyAngle;
    public int doRun;
    bool isRun;
    public Player nmyScript;
    int role;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerScript = GetComponent<Player>();
        funcDist = defDist;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = player.transform.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        nmyAngle =  player.localEulerAngles.z-angle;
        if (Vector3.Distance(player.transform.position, transform.position) < 3f
            )
        {
            if (nmyScript.dir < 0)
            {
                if (nmyAngle < 190 && nmyAngle > 90)
                    doRun = 3;
            }else
            {
                {
                    if (nmyAngle < 90 && nmyAngle > -10)
                        doRun = 3;
                }
            }
        }

        if (!playerScript.choose&&playerScript.cdel<1)
        {
            if (doRun>0)
            {
                spd = negSpd; doRun--;
            }
            if (del < 1)
            {
                if (doRun<1)
                {
                    if (Vector3.Distance(player.transform.position, transform.position) < funcDist) {
                        if (!isRun) { del = Random.RandomRange(0, 5);
                            if (role ==1||role==2) { playerScript.cast = true; }
                        }
                        isRun = true; spd = negSpd;
                    }
                    else
                    { if (isRun) { del = Random.RandomRange(0, 5); } isRun = false; spd = defSpd; }
                }
            }else { del--; }
            if (angle > 22.5 && angle < 157.5)
            {
                if (angle > 112.5 && angle < 157.5) { rb.velocity = new Vector2(-mm * spd, mm * spd); }
                else
                if (angle > 22.5 && angle < 67.5) { rb.velocity = new Vector2(mm * spd, mm * spd); }
                else
                { rb.velocity = new Vector2(0, 3 * spd); }
            }
            else
            if (angle < -22.5 && angle > -157.5)
            {
                if (angle > 112.5 && angle < 157.5) { rb.velocity = new Vector2(-mm * spd, -mm * spd); }
                if (angle > 22.5 && angle < 67.5) { rb.velocity = new Vector2(mm * spd, -mm * spd); }
                { rb.velocity = new Vector2(0, -3 * spd); }
            }
            else
            {
                if (angle > -22.5 && angle < 22.5) { rb.velocity = new Vector2(3 * spd, 0); }
                else
                if (angle > 157.5 || angle < -157.5) { rb.velocity = new Vector2(-3 * spd, 0); }
            }
        }
        if (every2)
        {
            role = playerScript.role;

            if (playerScript.role ==1)
            {
                if (playerScript.cd < 1) { funcDist = defDist - 0.5f; } else { funcDist = defDist; }
            } else if (playerScript.role==2)
            {
                defDist = 1.3f; funcDist = defDist;
            } else if (role==10)
            {
                if (playerScript.cd < 1) {
                    defDist = 1.5f; funcDist = defDist;
                } else
                {
                    defDist = 1.2f; funcDist = defDist;
                }
            }

            spd = playerScript.spd;
            defSpd = spd;
            negSpd = spd * -1;

            every2 = false;
        } else
        {
            every2 = true;
        }
    }
}
