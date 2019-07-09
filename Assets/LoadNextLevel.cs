using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextLevel : MonoBehaviour
{
    public void Load(){
        GameManager.instance.LevelCompleted();
    }
}
