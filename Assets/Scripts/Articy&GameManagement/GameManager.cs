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
    int levelToLoad;
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
        levelToLoad = PlayerPrefs.GetInt("currentLevelID", 0);
    }
    public void LoadScene(string sceneName, bool withLoadingScreen = true){
        if(currentScene != null)
            SceneManager.UnloadSceneAsync(currentScene);
        currentScene = sceneName;
        if(withLoadingScreen)
            StartCoroutine(LoadAsync(sceneName));
        else SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        isLoading = true;
    }

    public void LoadCurrentScene(){
        if(currentScene != null)
            SceneManager.UnloadSceneAsync(currentScene);
        currentScene = LevelToLoadString();
            StartCoroutine(LoadAsync(currentScene));
        isLoading = true;
    }

    public void LoadNewScene(){
        if(currentScene != null)
            SceneManager.UnloadSceneAsync(currentScene);
        levelToLoad = 0;
        PlayerPrefs.SetInt("currentLevelID", levelToLoad);
        currentScene = LevelToLoadString();
            StartCoroutine(LoadAsync(currentScene));
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

    public void LevelCompleted(){
        levelToLoad++;
        PlayerPrefs.SetInt("currentLevelID", levelToLoad);
    }
    string LevelToLoadString(){
        switch(levelToLoad){
            case 0: return "Level1";
            case 1: return "Interlude1";
            case 2: return "Level2";
        }
        return "oh fuck";
    }
}
