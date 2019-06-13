using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public GameObject puzzlePrefab;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<PuzzleWindow>().Activate(puzzlePrefab);
        }

    }
}
