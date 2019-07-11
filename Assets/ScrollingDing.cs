using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollingDing : MonoBehaviour
{
    [SerializeField] private float blinkTime = 0.4f;
    float nextblink;
    Image img;
    private void OnEnable() {
        nextblink = Time.time;
        img = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextblink){
            nextblink += blinkTime;
            img.enabled = !img.enabled;
        }
    }
}
