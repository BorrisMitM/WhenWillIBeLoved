﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSetter : MonoBehaviour
{
    public Location loc;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        FindObjectOfType<SongManager>().SetLocation(loc);
    }
}
