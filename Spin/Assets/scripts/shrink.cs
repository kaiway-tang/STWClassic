using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrink : MonoBehaviour
{
    float timer;
    public float x;
    public float y;
    bool go;
    public Player[] player;
    // Start is called before the first frame update
    void Start()
    {
        player[0] = Player.player1Stc;
        player[1] = Player.player2Stc;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (go)
        {
            if (timer < 60) { timer += 1 * Time.deltaTime; }
            else
            {
                transform.position += new Vector3(x, y, 0);
            }
        } else if (!player[0].choose && !player[1].choose) { go = true; }
    }
}
