using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject textPanel;
    private float fadeInDuration = 0.2f;
    bool playerInside = false;
    private void Update() {
        if(playerInside && Input.GetButtonDown("Interact")){
            textPanel.SetActive(true);
            StartCoroutine(ScaleFadeIn(textPanel.transform.localScale));
            GameManager.instance.puzzleActive = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")) playerInside = false;
    }

    IEnumerator ScaleFadeIn(Vector3 originalScale){
        float startTime = Time.time;
        while(Time.time <= startTime + fadeInDuration){
            textPanel.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, (Time.time - startTime) / fadeInDuration);
            yield return null;
        }
        textPanel.transform.localScale = originalScale;
    }
}
