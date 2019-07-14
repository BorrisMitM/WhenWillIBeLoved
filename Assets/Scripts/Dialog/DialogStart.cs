using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogStart : MonoBehaviour
{
    private bool playerInRange = false;

    private InteractionPopUp popUp;

    AudioSource source;

    private void Start() {
        popUp = FindObjectOfType<InteractionPopUp>();
        source = GetComponent<AudioSource>();
    }

    private void Update() {

        if(playerInRange && Input.GetButtonDown("Interact") && GameManager.instance.nextDialogUnlocked){

            FindObjectOfType<ArticyManager>().StartDialog();

            source.Play();

            popUp.Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && GameManager.instance.nextDialogUnlocked) {
            playerInRange = true;
            popUp.Activate(PopUp.Talk);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            playerInRange = false;
            popUp.Deactivate();
        }        
    }

}
