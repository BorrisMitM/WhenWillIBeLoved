using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFadeOut : MonoBehaviour
{
    
    public float waitTime;
    public float newAlpha;

    [Range(0f,1f)]
    public float step;

    [ContextMenu("FadeOut")]
    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    [ContextMenu("FadeIn")]
    public void FadeIn()
    {
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);

        }
    }

    public IEnumerator FadeOut()
    {
        newAlpha = 1f;

        while (newAlpha >= 0)
        {
            foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            {

                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
                
            }

            FindObjectOfType<Light>().intensity = Mathf.Lerp(0.3f, 2.5f, newAlpha);
            newAlpha -= step;
            yield return new WaitForSeconds(waitTime);
        }


        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {

            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);

        }
    }
}
