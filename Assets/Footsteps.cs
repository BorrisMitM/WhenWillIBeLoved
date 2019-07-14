using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    [SerializeField]
    AudioClip[] steps;

    [SerializeField]
    AudioSource currentAudio;



    public void StepSound()
    {
        currentAudio.clip = steps[Random.Range(0, steps.Length)];
        currentAudio.volume = 0.1f;
        currentAudio.Play();

    }


}
