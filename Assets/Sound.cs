using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource audio;

    private void OnDisable()
    {
        audio.Play();
    }
}
