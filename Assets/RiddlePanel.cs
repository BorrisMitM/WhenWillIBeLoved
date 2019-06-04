using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddlePanel : MonoBehaviour
{
    [ContextMenu("activate")]
    public void Activate(){
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
