using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelEnd : MonoBehaviour
{
    public GameObject[] objsToActivate;
    public GameObject endPanel;

    [SerializeField] private string[] variableNames;
    [SerializeField] private string[] onTextNames;
    [SerializeField] private Vector2 targetPosGlenn;
    [SerializeField] private Transform glenn;
    [SerializeField] private bool interlude = false;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip absorb;
    [SerializeField] AudioClip success;

    public delegate void LevelEnding();
    public static event LevelEnding OnLevelEnding;
    private void OnEnable() {
        ArticyManager.OnDialogEnded += OnDialogEnded;
    }
    private void OnDisable() {
        ArticyManager.OnDialogEnded -= OnDialogEnded;
    }
    
    private void OnDialogEnded(){
        if (interlude)
        {
            GetComponent<LevelFadeOut>().StartFadeOut();
            NewLevel();
            return;
        }

        if (FindObjectOfType<ArticyManager>().LastDialogPlayed())
        {
            OnLevelEnding();
            GetComponent<LevelFadeOut>().StartFadeOut();
            foreach(GameObject obj in objsToActivate){
                obj.SetActive(true);
            }
            source.clip = absorb;
            source.Play();

            FindObjectOfType<CameraShake>().ShakeCamera(2f, .1f);
            StartCoroutine(ActivateEndPanel());
            StartCoroutine(MoveGlenn());
        }
    }

    [ContextMenu("LevelEnd")]
    private void OnDialogEndedTest()
    {
        if (interlude)
        {
            GetComponent<LevelFadeOut>().StartFadeOut();
            NewLevel();
            return;
        }
        {
            OnLevelEnding();
            GetComponent<LevelFadeOut>().StartFadeOut();
            foreach (GameObject obj in objsToActivate)
            {
                obj.SetActive(true);
            }

            source.clip = absorb;
            source.Play();

            FindObjectOfType<CameraShake>().ShakeCamera(2f, .1f);
            StartCoroutine(ActivateEndPanel());
            StartCoroutine(MoveGlenn());
        }
    }

    public void NewLevel()
    {
        StartCoroutine(LoadNewLevel());
    }
    IEnumerator LoadNewLevel()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.LevelCompleted();
    }
    IEnumerator MoveGlenn()
    {
        float startTime = Time.time;
        float duration = 1f;
        Vector3 startPos = glenn.position;
        glenn.GetComponent<IsometricPlayerMovementController>().isoRenderer.SetDirection(new Vector2(1f, 1f));
        while(Time.time <= startTime + duration)
        {
            glenn.position = Vector3.Lerp(startPos, targetPosGlenn, (Time.time - startTime) / duration);
            yield return null;
        }
    }
    IEnumerator ActivateEndPanel(){
        yield return new WaitForSeconds(3f);
        endPanel.SetActive(true);

        source.clip = success;
        source.Play();

        TextMeshProUGUI text = endPanel.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "You gathered " + GetItem() + " from Emilia.";
    }
    string GetItem(){
        int maxValue = GetGlobalVariable.Int(variableNames[0]);
        int index = 0;
        for (int i = 1; i < variableNames.Length; i++)
        {
            int newValue = GetGlobalVariable.Int("GlobalVariables." + variableNames[i]);
            if(newValue > maxValue) {
                maxValue = newValue;
                index = i;
            }
        }
        return onTextNames[index];
    }
}
