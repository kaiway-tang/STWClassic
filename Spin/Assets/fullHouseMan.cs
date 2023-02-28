using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fullHouseMan : MonoBehaviour
{
    //graves key + R to reset
    public Player[] playerScr;
    public GameObject[] lists;
    public SpriteRenderer[] p1Classes;
    public SpriteRenderer[] p2Classes;
    public int[] select;
    public Sprite[] classNames;

    public static fullHouseMan fullHouseStc;

    bool every2 = false;
    // Start is called before the first frame update
    void Start()
    {
        fullHouseStc = GetComponent<fullHouseMan>();
    }
    private void Update()
    {
        if (playerScr[0].choose)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                p1Classes[select[0]].sprite = classNames[playerScr[0].roleS-1];
                select[0]++;
            }
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                p2Classes[select[1]].sprite = classNames[playerScr[1].roleS - 1];
                select[1]++;
            }
            if (Input.GetKey(KeyCode.BackQuote) && Input.GetKeyDown(KeyCode.R)) { resetList(); }
        }
    }
    void FixedUpdate()
    {
        
    }
    private void resetList()
    {
        for (int i = 0; i < 5; i++)
        {
            p1Classes[i].sprite = null;
            p2Classes[i].sprite = null;
            p1Classes[i].color = new Color(p1Classes[i].color.r, p1Classes[i].color.g, p1Classes[i].color.b, 1);
            p2Classes[i].color -= new Color(p2Classes[i].color.r, p2Classes[i].color.g, p2Classes[i].color.b, 1);
        }
        select[0] = 0;
        select[1] = 0;
    }
    public void listVisible(bool pVisible)
    {
        lists[0].SetActive(pVisible);
        lists[1].SetActive(pVisible);
    }
    public void listVisible(int listID, bool pVisible)
    {
        lists[listID].SetActive(pVisible);
    }
    public void lost(int listID, int role)
    {
        for (int i = 0; i < 5; i++)
        {
            if (listID == 0)
            {
                if (p1Classes[i].sprite == classNames[role - 1])
                {
                    p1Classes[i].color = new Color(p1Classes[i].color.r, p1Classes[i].color.g, p1Classes[i].color.b , 0.5f);
                }
            }
            if (listID == 1)
            {
                if (p2Classes[i].sprite == classNames[role - 1])
                {
                    p2Classes[i].color = new Color(p2Classes[i].color.r, p2Classes[i].color.g, p2Classes[i].color.b, 0.5f);
                }
            }
        }
    }
}
