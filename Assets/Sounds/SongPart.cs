using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Location { LivingRoom, Kitchen, Bedroom, Tent};
[CreateAssetMenu(menuName = "SongPart")]

public class SongPart : ScriptableObject
{
    public List<Location> locationsPlayed;
    public AudioClip clip;
    public bool isPlaying;
    public AudioSource source;



}
