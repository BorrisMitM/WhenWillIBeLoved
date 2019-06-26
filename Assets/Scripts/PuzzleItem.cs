using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Glow))]
public class PuzzleItem : MonoBehaviour
{
    public GameObject puzzlePrefab;

    bool playerClose = false;

    private void Update() {
        if (playerClose && Input.GetButtonDown("Interact"))
        {
            FindObjectOfType<PuzzleWindow>().Activate(puzzlePrefab);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){ 
            playerClose = true;
            GetComponent<Glow>().StartGlow();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){ 
            playerClose = false;
            GetComponent<Glow>().EndGlow();
        }
    }
}
