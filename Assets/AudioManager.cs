using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<SoundContainer> soundContainers;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
            return;
        }

        foreach (SoundContainer sound in soundContainers)
        {
           sound.source =  gameObject.AddComponent<AudioSource>();

           sound.source.clip = sound.audio;
           sound.source.volume = sound.volume;
           sound.source.loop = sound.loop;

        }
    }

    private void Start()
    {
        PlaySound("Piano");
    }


    public void PlaySound(string name)
    {
        SoundContainer playingSound = soundContainers.Find(sound => sound.clipName == name);
        if (playingSound == null)
        {
            Debug.LogWarning("Couldn't find the sound:" + name);
            return;
        }

        playingSound.source.Play();

    }
}


