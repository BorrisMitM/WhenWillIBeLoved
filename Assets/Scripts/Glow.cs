using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : MonoBehaviour
{
    public void StartGlow(){
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetInt("_ShowOutline", 1);
    }

    public void EndGlow(){
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetInt("_ShowOutline", 0);

    }
}
