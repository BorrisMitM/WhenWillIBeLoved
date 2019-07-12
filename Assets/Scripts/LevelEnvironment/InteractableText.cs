using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableText : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact")){
            GameManager.instance.puzzleActive = false;
            gameObject.SetActive(false);
        }
    }
}
