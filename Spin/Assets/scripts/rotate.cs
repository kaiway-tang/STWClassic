using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public int deg;
    public bool healPatch;
    public Player[] player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (healPatch) { if (player[0].choose||player[1].choose) { return; } }
        transform.Rotate(Vector3.forward*deg*Time.deltaTime);
    }
}
