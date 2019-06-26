using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Glow))]
public class ChestRiddle : MonoBehaviour
{

    bool playerClose = false;

    private void Update() {
        if (playerClose && Input.GetButtonDown("Interact"))
        {
            FindObjectOfType<RiddlePanel>().Activate();
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
