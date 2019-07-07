using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]

public class SoundContainer
{
    public string clipName;

    public AudioClip audio;

    public bool loop;

    [Range(0f,1f)]
    public float volume;

    [HideInInspector]
    public string emotionMeterString;
    [HideInInspector]
    public AudioSource source;
}
