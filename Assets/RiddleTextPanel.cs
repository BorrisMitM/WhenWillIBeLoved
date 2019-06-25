using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RiddleTextPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    private float closeOffsetTimer;
    
    public void EnablePanel(string newText){
        panel.SetActive(true);
        text.text = newText;
        closeOffsetTimer = Time.time + 1f;
        GameManager.instance.puzzleActive = true;
    }

    private void Update() {
        if(Input.GetButtonDown("Interact") && Time.time > closeOffsetTimer){
            if(panel.activeSelf){
                panel.SetActive(false);
                GameManager.instance.puzzleActive = false;
            }
        }
    }
}
