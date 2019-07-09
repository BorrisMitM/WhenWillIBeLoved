using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelEnd : MonoBehaviour
{
    public GameObject[] objsToActivate;
    public GameObject endPanel;
    private void OnEnable() {
        ArticyManager.OnDialogEnded += OnDialogEnded;
    }
    private void OnDisable() {
        ArticyManager.OnDialogEnded -= OnDialogEnded;
    }

    [ContextMenu("LevelEnd")]
    private void OnDialogEnded(){
        //if(FindObjectOfType<ArticyManager>().LastDialogPlayed())
        {
            GetComponent<LevelFadeOut>().StartFadeOut();
            foreach(GameObject obj in objsToActivate){
                obj.SetActive(true);
            }
            FindObjectOfType<CameraShake>().ShakeCamera(2f, .1f);
            StartCoroutine(ActivateEndPanel());
        }
    }

    IEnumerator ActivateEndPanel(){
        yield return new WaitForSeconds(3f);
        endPanel.SetActive(true);
        TextMeshProUGUI text = endPanel.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "You gathered ANGER from Emilia.";
    }
}
