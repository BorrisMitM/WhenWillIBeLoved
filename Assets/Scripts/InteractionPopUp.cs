using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPopUp : MonoBehaviour
{
    [SerializeField] private GameObject popUp;

    float fadeInDuration = 0.1f;

    Vector3 originalScale;
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
        popUp.SetActive(false);
    }

    private void Awake()
    {
        originalScale = popUp.transform.localScale;
    }

    public void Activate()
    {
        if (!active) return;
        popUp.SetActive(true);
        StartCoroutine(ScaleFadeIn());
    }

    public void Deactivate()
    {
        popUp.SetActive(false);
    }


    IEnumerator ScaleFadeIn()
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
