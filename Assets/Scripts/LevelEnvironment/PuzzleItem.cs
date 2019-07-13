using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public List<GameObject> puzzlePrefab;
    public List<string> inbetweenTexts;
    public string tutorialText;
    public bool played = false;
    bool playerClose = false;
    private InteractionPopUp popUp;

    private void Start()
    {
        popUp = FindObjectOfType<InteractionPopUp>();
    }

    private void Update() {
        if (playerClose && Input.GetButtonDown("Interact") && !GameManager.instance.puzzleActive && !played)
        {
            FindObjectOfType<PuzzleWindow>().Activate(puzzlePrefab, inbetweenTexts, tutorialText, this);
            popUp.Deactivate();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !played){ 
            playerClose = true;
            popUp.Activate(PopUp.ImportantInteraction);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){ 
            playerClose = false;
            popUp.Deactivate();
        }
    }
}
