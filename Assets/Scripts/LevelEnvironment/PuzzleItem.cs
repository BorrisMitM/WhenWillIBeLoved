using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Glow))]
public class PuzzleItem : MonoBehaviour
{
    public List<GameObject> puzzlePrefab;
    public List<string> inbetweenTexts;
    public string tutorialText;
    public bool played = false;
    bool playerClose = false;

    private void Update() {
        if (playerClose && Input.GetButtonDown("Interact") && !GameManager.instance.puzzleActive && !played)
        {
            FindObjectOfType<PuzzleWindow>().Activate(puzzlePrefab, inbetweenTexts, tutorialText, this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !played){ 
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
