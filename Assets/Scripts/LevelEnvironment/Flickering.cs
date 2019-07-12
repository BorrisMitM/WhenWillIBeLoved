using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flickering : MonoBehaviour
{
    public float waitingTime;
    public bool lightOn;
    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (Random.Range(0, 2) == 1)
        {
            lightOn = true;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            waitingTime = Random.Range(2f, 15f);
        }

        else
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            waitingTime = Random.Range(0f, 1f);
        }
        
    }


    void Update()
    {
        waitingTime -= Time.deltaTime;

        if (waitingTime <= 0)
        {
            lightOn = !lightOn;

            if (lightOn)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                waitingTime = Random.Range(2f, 15f);
            }

            else
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
                waitingTime = Random.Range(0f, 1f);
            }
        }
        
    }
}
