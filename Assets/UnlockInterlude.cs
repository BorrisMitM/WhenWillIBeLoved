using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockInterlude : MonoBehaviour
{
    private bool playerInRange = false;
    public static bool unlocked = false;
    private void Update()
    {
        if (playerInRange && Input.GetButtonDown("Interact"))
        {
            unlocked = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && GameManager.instance.nextDialogUnlocked)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
