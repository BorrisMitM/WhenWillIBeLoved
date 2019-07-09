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
    [ContextMenu("Increase")]
    public void IncreaseSaturation(){
        StartCoroutine(FadeInColor());
    }

    IEnumerator FadeInColor(){
        float startTime = Time.time;
        float finalSaturation = (colorGrading.saturation.value + (saturation + 100f) / 3f);
        while(Time.time < startTime + fadeInTime){
            colorGrading.saturation.value = Mathf.Lerp(colorGrading.saturation.value, finalSaturation, (Time.time - startTime) / fadeInTime);
            yield return null;
        }
        colorGrading.saturation.value = finalSaturation;
    }
}
