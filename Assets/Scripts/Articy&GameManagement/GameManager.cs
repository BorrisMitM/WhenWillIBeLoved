using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Articy.Side_Effects;
using Articy.Unity;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    string currentScene;

    public bool puzzleActive;
    public bool nextDialogUnlocked = true;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image glennSprite;
    [SerializeField] private TextMeshProUGUI loadText;
    [SerializeField] private List<Sprite> glennSprites;
    [SerializeField] private float spriteChangePause = .4f;
    private bool isLoading = false;
    private void Awake() {
        if(instance == null)
            instance = this;
        else{
            Debug.LogWarning("Multiple instances of the GameManager active.");
            Destroy(this);
        }
    }
    public void LoadScene(string sceneName){
        if(currentScene != null)
            SceneManager.UnloadSceneAsync(currentScene);
        currentScene = sceneName;
        //SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        StartCoroutine(LoadAsync(sceneName));
        isLoading = true;
    }

    private void Update() {
        if(isLoading && Input.anyKeyDown){
            StopAllCoroutines();
            panel.SetActive(false);
            isLoading = false;
        }
    }

    IEnumerator LoadAsync(string sceneName){
        panel.SetActive(true);
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        float lastSpriteUpdate = Time.time;
        int counter = 0;
        while(!op.isDone){
            float progress = Mathf.Clamp01(op.progress / .9f);
            if(Time.time > lastSpriteUpdate + spriteChangePause){
                lastSpriteUpdate += spriteChangePause;
                counter++;
                if(counter > glennSprites.Count) counter = 0;
                glennSprite.sprite = glennSprites[counter];
            }
            loadText.text = (progress * 100f).ToString() + "%";
            yield return null;
        }
        //panel.SetActive(false);
        loadText.text = "Press any key.";
    }
}
