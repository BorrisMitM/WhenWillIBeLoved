using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogStart : MonoBehaviour
{
    private bool playerInRange = false;

    private Glow glow;

    private void Start() {
        glow = GetComponent<Glow>();
    }

    private void Update() {
        if(playerInRange && Input.GetButtonDown("Interact") && GameManager.instance.nextDialogUnlocked){
            FindObjectOfType<ArticyManager>().StartDialog();
            glow.EndGlow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && GameManager.instance.nextDialogUnlocked) {
            playerInRange = true;
            glow.StartGlow();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerInRange = false;
            glow.EndGlow();
        }        
    }

}
