using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject textPanel;
    private float fadeInDuration = 0.2f;
    bool playerInside = false;
    Vector3 originalScale;
    InteractionPopUp popUp;
    private void Awake() {
        originalScale = textPanel.transform.localScale;
        popUp = FindObjectOfType<InteractionPopUp>();
    }
    private void Update() {
        if(playerInside && Input.GetButtonDown("Interact")){
            textPanel.SetActive(true);
            StartCoroutine(ScaleFadeIn());
            GameManager.instance.puzzleActive = true;
            popUp.Deactivate();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            popUp.Activate(PopUp.Fluff);
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            popUp.Deactivate();
            playerInside = false;
        }
    }

    IEnumerator ScaleFadeIn(){
        float startTime = Time.time;
        while(Time.time <= startTime + fadeInDuration){
            textPanel.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, (Time.time - startTime) / fadeInDuration);
            yield return null;
        }
        textPanel.transform.localScale = originalScale;
    }
}
