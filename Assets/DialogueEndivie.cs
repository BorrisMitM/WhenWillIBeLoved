using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEndivie : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("entered");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<DialogSystem>().StartDialog();
        }
    }
}
