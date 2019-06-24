using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class AppearingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject blockingCollider;
    public enum ChangeOn{boolTrue, boolFalse, intLower, intHigher};
    [SerializeField] public ChangeOn changeOn;
    [HideInInspector]
    public int limit;
    [SerializeField] private string variableToAppearOn;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float shakeMagnitude = .4f;
    private bool triggered = false;
    private CameraMovement camMove;
    private CameraShake camShake;
    private void Start() {
        switch(changeOn){
            case ChangeOn.boolTrue : 
                        if(GetGlobalVariable.Bool(variableToAppearOn)) triggered = true;
                        break;
            case ChangeOn.boolFalse : 
                        if(!GetGlobalVariable.Bool(variableToAppearOn)) triggered = true;
                        break;
            case ChangeOn.intLower : 
                        if(GetGlobalVariable.Int(variableToAppearOn) < limit) triggered = true;
                        break;
            case ChangeOn.intHigher : 
                        if(GetGlobalVariable.Int(variableToAppearOn) > limit) triggered = true;
                        break;
        }
        Appear(triggered);
        camMove = FindObjectOfType<CameraMovement>();
        camShake = FindObjectOfType<CameraShake>();
    }
    // Update is called once per frame
    void Update()
    {
        if(triggered) return;
        switch(changeOn){
            case ChangeOn.boolTrue : 
                        if(GetGlobalVariable.Bool(variableToAppearOn)) triggered = true;
                        break;
            case ChangeOn.boolFalse : 
                        if(!GetGlobalVariable.Bool(variableToAppearOn)) triggered = true;
                        break;
            case ChangeOn.intLower : 
                        if(GetGlobalVariable.Int(variableToAppearOn) < limit) triggered = true;
                        break;
            case ChangeOn.intHigher : 
                        if(GetGlobalVariable.Int(variableToAppearOn) > limit) triggered = true;
                        break;
        }
        if(triggered)
            Appear(true);
    }

    private void Appear(bool appear)
    {
        platform.SetActive(appear);
        if(appear){
            camMove.platformToFocus = platform.transform;
            StartCoroutine(camShake.ShakeCamera(fadeInDuration, shakeMagnitude));
            StartCoroutine(FadeIn(appear));
        }
        else blockingCollider.SetActive(true);
    }

    IEnumerator FadeIn(bool appear){
        float startTime = Time.time;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        while(Time.time < startTime + fadeInDuration){
            float alpha = appear ? Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeInDuration) : Mathf.Lerp(1f, 0f, (Time.time - startTime) / fadeInDuration);
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = new Color(sprite.color.r,sprite.color.g,sprite.color.b, alpha);
            }
            yield return new WaitForSeconds(0.05f);
        }
        foreach(SpriteRenderer sprite in sprites){
            sprite.color = new Color(sprite.color.r,sprite.color.g,sprite.color.b, 1f);
        }
        blockingCollider.SetActive(!appear);
        camMove.platformToFocus = null;
    }
}

[CustomEditor(typeof(AppearingPlatform))]
public class AppearingPlatformEditor : Editor {
    AppearingPlatform platform;
    public override void OnInspectorGUI() {
        platform = target as AppearingPlatform;
        if(platform.changeOn == AppearingPlatform.ChangeOn.boolFalse || platform.changeOn == AppearingPlatform.ChangeOn.boolTrue){
            base.OnInspectorGUI();
        }
        else{
            base.OnInspectorGUI();
            platform.limit = EditorGUILayout.IntField("limit", platform.limit);
        }
    }
}