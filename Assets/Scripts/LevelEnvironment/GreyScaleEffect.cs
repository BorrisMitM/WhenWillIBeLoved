using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class GreyScaleEffect : MonoBehaviour
{
    float saturation;
    [SerializeField] private PostProcessProfile profile;
    [SerializeField] private float fadeInTime = 1f;
    [SerializeField] private bool active = false;
    ColorGrading colorGrading;


    private void Awake() {
        if(!active) return;
        profile = GetComponent<PostProcessVolume>().profile;
        profile.TryGetSettings(out colorGrading);
        if(colorGrading){
            saturation = colorGrading.saturation.value;
            colorGrading.saturation.value = (-100f);
        }
    }
    [ContextMenu("33")]
    public void FadeIn33()
    {
        StartCoroutine(FadeInColor(-33));
    }
    [ContextMenu("66")]
    public void FadeIn66()
    {
        StartCoroutine(FadeInColor(-66));
    }
    IEnumerator FadeInColor(float finalSaturation){
        float startTime = Time.time;
        //float finalSaturation = (colorGrading.saturation.value + (saturation + 100f) / 3f);
        while(Time.time < startTime + fadeInTime){
            colorGrading.saturation.value = Mathf.Lerp(colorGrading.saturation.value, finalSaturation, (Time.time - startTime) / fadeInTime);
            yield return null;
        }
        colorGrading.saturation.value = finalSaturation;
    }

    private void OnEnable() {
        ArticyManager.OnDialogEnded += OnDialogEnded;
    }
    private void OnDisable() {
        ArticyManager.OnDialogEnded += OnDialogEnded;
    }

    void OnDialogEnded(){
        if(GetGlobalVariable.Bool("GlobalVariables.Platform6")) StartCoroutine(FadeInColor(0));
        else if(GetGlobalVariable.Bool("GlobalVariables.Platform5")) StartCoroutine(FadeInColor(-33));
        else if(GetGlobalVariable.Bool("GlobalVariables.Platform4")) StartCoroutine(FadeInColor(-66));
    }
}
