using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour { 


    private void OnMouseDown()
    {
        FindObjectOfType<PuzzleWindow>().Deactivate();
        
    }
}
