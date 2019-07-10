using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelEnd : MonoBehaviour
{
    public GameObject[] objsToActivate;
    public GameObject endPanel;

    [SerializeField] private string[] variableNames;
    [SerializeField] private string[] onTextNames;
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
        text.text = "You gathered " + GetItem() + " from Emilia.";
    }
    string GetItem(){
        int maxValue = GetGlobalVariable.Int(variableNames[0]);
        int index = 0;
        for (int i = 1; i < variableNames.Length; i++)
        {
            int newValue = GetGlobalVariable.Int(variableNames[i]);
            if(newValue > maxValue) {
                maxValue = newValue;
                index = i;
            }
        }
        return onTextNames[index];
    }
}
