using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    private SingleScreenDialog[] dialogs;
    [SerializeField]
    private List<Answers> answers;
    [SerializeField]
    private GameObject contextMenu;
    [SerializeField]
    private int charactersPerSecond = 7;
    [SerializeField]
    private GameObject panel;
    private int currentDialog = 0;
    bool isScrolling = false;
    Coroutine scrollingCo;
    [SerializeField]
    private Image portrait;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(panel.activeSelf == false) return;
        if(Input.GetKeyDown(KeyCode.Space)){
            if(isScrolling){
                EndScroll();
            }
            else {
                if(contextMenu.activeSelf == true) return;
                foreach(Answers ans in answers){
                    if(currentDialog == ans.dialogId){
                        contextMenu.SetActive(true);
                        foreach(GameObject answerButton in ans.answerButtons){
                            Instantiate(answerButton, contextMenu.transform);
                        }
                        answers.Remove(ans);
                        return;
                    }
                }
                StartScroll();
            }
        }
    }

    [ContextMenu("start")]
    public void StartDialog(){
        panel.SetActive(true);
        StartScroll();
    }
    public void StartScroll(){
        if(scrollingCo != null) return;
        if(dialogs.Length == 0 || currentDialog >= dialogs.Length){
            DeactivateDialog();
            return;
        }
        portrait.sprite = dialogs[currentDialog].portrait;
        scrollingCo = StartCoroutine(Scrolling());
    }

    private void EndScroll()
    {
        if(scrollingCo!=null) 
            StopCoroutine(scrollingCo);
        scrollingCo = null;
        if(dialogs.Length == 0 || currentDialog >= dialogs.Length){
            DeactivateDialog();
            return;
        }
        text.text = dialogs[currentDialog].text;
        isScrolling = false;
        currentDialog++;
    }

    private void DeactivateDialog(){
        isScrolling = false;
        panel.gameObject.SetActive(false);
    }
    IEnumerator Scrolling(){
        isScrolling = true;
        int currentChar = 0;
        while(currentChar < dialogs[currentDialog].text.Length){
            text.text = dialogs[currentDialog].text.Substring(0, currentChar);
            yield return new WaitForSeconds(1f / charactersPerSecond);
            currentChar++;
        }
        EndScroll();
    }
}
