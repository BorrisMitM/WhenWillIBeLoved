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
    private bool triggered = false;

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
        blockingCollider.SetActive(!appear);
    }

    IEnumerator FadeIn(){
        float startTime = Time.time + fadeInDuration;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        while(Time.time < startTime){
            float alpha = Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeInDuration);
            foreach(SpriteRenderer sprite in sprites){
                sprite.color = new Color(sprite.color.r,sprite.color.g,sprite.color.b, alpha);
            }
            yield return null;
        }
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