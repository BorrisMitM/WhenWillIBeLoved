using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float amplitude;
    public float frequency;

    Vector3 originalPos;
    Vector3 newPos;

    float PosOffset;
    

    void Start()
    {
        originalPos = transform.position;
        PosOffset = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        newPos = originalPos;
        newPos.y += Mathf.Sin((Time.time * Mathf.PI * frequency)+ PosOffset) * amplitude;

        transform.position = newPos;
    }
}
