﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

    public List<SongPart> songParts;
    public Location startLoc;

    //private Location loc;


    private void Awake()
    {
        foreach (SongPart songs in songParts)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            songs.source = source;
            songs.isPlaying = false;
            source.volume = 0;
            source.loop = true;
            source.clip = songs.clip;
            source.Play();
        }

        SetLocation(startLoc);
    }

    public void SetLocation(Location newLoc )
    {
        //loc = _loc;

        foreach (SongPart songs in songParts)
        {
            bool inLocation = false;

            foreach(Location loc in songs.locationsPlayed)
            {
                if (loc == newLoc) inLocation = true;
            }

            if(inLocation == true && !songs.isPlaying)
            {
                songs.source.volume = 1;
                songs.isPlaying = true;
            }

            else if (!inLocation && songs.isPlaying)
            {
                songs.source.volume = 0;
                songs.isPlaying = false;
            }

       
        }
       
    }
}
