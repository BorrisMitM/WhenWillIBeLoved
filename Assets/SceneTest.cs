﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.LoadScene("Level1");
    }

}