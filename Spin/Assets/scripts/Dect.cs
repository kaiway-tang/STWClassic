using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dect : MonoBehaviour
{
    public Player player;
    public GameObject ind;
    public Sprite square;
    int del;
    public bool urg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = player.target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
    }
}
