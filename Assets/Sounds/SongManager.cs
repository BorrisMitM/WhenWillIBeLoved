using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

    public List<SongPart> songParts;
    public Location startLoc;
    public AudioSource source;

    //private Location loc;


    private void Awake()
    {
        foreach (SongPart songs in songParts)
        {
            source = gameObject.AddComponent<AudioSource>();
            songs.source = this.source;
            songs.isPlaying = false;
            source.volume = 0;
            source.loop = true;

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
                source.volume = 1;
                songs.isPlaying = true;
            }

            else if (!inLocation && songs.isPlaying)
            {
                source.volume = 0;
                songs.isPlaying = false;
            }

       
        }
       
    }
}
