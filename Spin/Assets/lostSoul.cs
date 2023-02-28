using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lostSoul : MonoBehaviour
{
    int life;
    public Transform player;
    bool every2;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.forward * Random.Range(0, 360));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        life++;
        if (life<7)
        {
            transform.position += transform.up * .2f;
        } else
        {
            if (every2)
            {
                transform.position += transform.up * .05f;
                Vector2 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 4f * Time.deltaTime);
                every2 = false;
            }
            else
            {
                every2 = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player"&&life>6)
        {
            col.GetComponent<Player>().HB.GetComponent<xq>().HP += 3;
            Destroy(gameObject);
        }
    }
}
