using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseContextMenu : MonoBehaviour
{
    public void Close(){
        FindObjectOfType<DialogSystem>().StartScroll();
        GameObject.Find("ContextMenu").SetActive(false);
    }
}
