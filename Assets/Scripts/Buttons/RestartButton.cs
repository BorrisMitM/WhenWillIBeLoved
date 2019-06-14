using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{

    private void OnMouseDown()
    {
        FindObjectOfType<PuzzleWindow>().Restart();

    }

}
