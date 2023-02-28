using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlez : MonoBehaviour
{
    public int life;
    public ParticleSystem particles;
    int del;
    float loseLife;
    // Start is called before the first frame update
    void Start()
    {
        loseLife = particles.startLifetime / life * 1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //particles.startLifetime -= loseLife;
        if (transform.parent==null) { particles.loop = false; }
        del++;
        if (del>=life) { Destroy(gameObject); }
    }
}
