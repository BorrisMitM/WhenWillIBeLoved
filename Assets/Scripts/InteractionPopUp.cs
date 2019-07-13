using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopUp { Talk, ImportantInteraction, Fluff};
public class InteractionPopUp : MonoBehaviour
{
    [SerializeField] private GameObject talkPopUp;
    [SerializeField] private GameObject importantPopUp;
    [SerializeField] private GameObject fluffPopUp;

    float fadeInDuration = 0.1f;

    Vector3 talkScale;
    Vector3 importantScale;
    Vector3 fluffScale;
    bool active = true;
    private void OnEnable()
    {
        LevelEnd.OnLevelEnding += OnLevelEnding;
    }

    private void OnDisable()
    {
        LevelEnd.OnLevelEnding -= OnLevelEnding;
    }

    private void OnLevelEnding()
    {
        active = false;
        Deactivate();
    }

    private void Awake()
    {
        talkScale = talkPopUp.transform.localScale;
        importantScale = importantPopUp.transform.localScale;
        fluffScale = fluffPopUp.transform.localScale;
    }
    public void Activate(PopUp type)
    {
        if (!active) return;
        if (type == PopUp.Talk)
        {
            talkPopUp.SetActive(true);
            StartCoroutine(ScaleFadeIn(talkPopUp, talkScale));
        }
        else if (type == PopUp.ImportantInteraction)
        {
            importantPopUp.SetActive(true);
            StartCoroutine(ScaleFadeIn(importantPopUp, importantScale));
        }
        else if(type == PopUp.Fluff)
        {
            fluffPopUp.SetActive(true);
            StartCoroutine(ScaleFadeIn(fluffPopUp, fluffScale));
        }
    }

    public void Deactivate()
    {
        talkPopUp.SetActive(false);
        importantPopUp.SetActive(false);
        fluffPopUp.SetActive(false);
    }


    IEnumerator ScaleFadeIn(GameObject popUp, Vector3 originalScale)
    {
        float startTime = Time.time;
        while (Time.time <= startTime + fadeInDuration)
        {
            popUp.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, (Time.time - startTime) / fadeInDuration);
            yield return null;
        }
        popUp.transform.localScale = originalScale;
    }
}
