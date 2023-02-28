using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TourMan : MonoBehaviour
{
    public Player playScr;
    public GameObject HB1;
    public GameObject HB2;
    public GameObject p1;
    public GameObject p2;
    public int del;
    public string back;
    public GameObject border;
    public GameObject borderr;
    bool reset;
    public Transform healP;
    int dell;
    public bool fullHouse;
    public static bool fullHouseStc;
    Transform trfm;
    // Start is called before the first frame update
    void Start()
    {
        trfm = transform;
        fullHouseStc = fullHouse;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playScr.choose)
        {
            if (dell > 0) { dell += 1; }
            if (dell == 10)
            {
                p1.transform.position = new Vector3(-3, 0, 0);
                p2.transform.position = new Vector3(3, 0, 0);
                dell = 0;
            }
            if (HB1.GetComponent<xq>().HP <= 0 || HB2.GetComponent<xq>().HP <= 0)
            {
                if (del < 1 && dell < 1)
                {
                    del = 1;
                } //p1.transform.position = new Vector3(-300, 0, 0); p2.transform.position = new Vector3(-300, 0, 0);
            }
            if (del > 0) { del += 1; }
            if (del > 36)
            {
                Destroy(borderr);
                GameObject Border = Instantiate(border, trfm.position, trfm.rotation);
                borderr = Border;
                del = 0;
                Vector3 rotty = trfm.rotation.eulerAngles;
                rotty.z = 0.0f;
                healP.rotation = Quaternion.Euler(rotty);
                if (fullHouse) { if (HB1.GetComponent<xq>().HP < 70) { HB1.GetComponent<xq>().HP = 70; } if (HB2.GetComponent<xq>().HP < 70) { HB2.GetComponent<xq>().HP = 70; } }
                else
                {
                    HB1.GetComponent<xq>().HP = -1; HB2.GetComponent<xq>().HP = -1;
                }
                dell = 1;
                /*SceneManager.LoadScene(back);*/
            }
        }
    }
}
