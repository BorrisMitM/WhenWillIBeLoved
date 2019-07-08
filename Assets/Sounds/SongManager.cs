using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour {

    public List<SongPart> songParts;

    [HideInInspector]
    public AudioSource source;

    private Location loc;


    private void Awake()
    {
        foreach (SongPart songs in songParts)
        {
            source = gameObject.AddComponent<AudioSource>();
            source.volume = 0;
            source.loop = true;

            source.Play();
        }

    }

    public void SetLocation(Location _loc )
    {
        loc = _loc;

        foreach (SongPart songs in songParts)
        {
            foreach(Location loc in songs.locationsPlayed)
            {
                
            }
        }
       
    }
}
