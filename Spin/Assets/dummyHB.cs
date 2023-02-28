using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyHB : MonoBehaviour
{
    public int function;//0:[war]Front1 1:[war]Front2 2: shade (face nmy)
    public dummy dummy;
    public Transform nmyPly;
    Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (function == 2)
        {
            Vector2 direction = nmyPly.position - pos.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            pos.rotation = rotation;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag=="Player")
        {
            Debug.Log("hit");
            if (function==0||function==2)
            {
                dummy.playerScript.cast = true;
            } else if (function==1)
            {
                if (dummy.playerScript.comDel<1) { dummy.playerScript.cast = true; }
            }
        }
    }
}
