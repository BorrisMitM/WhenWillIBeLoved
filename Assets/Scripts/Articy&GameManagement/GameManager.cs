﻿using System.Collections;
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
    public bool disableMovement = false;
    public bool nextDialogUnlocked = true;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image glennSprite;
    [SerializeField] private TextMeshProUGUI loadText;
    [SerializeField] private List<Sprite> glennSprites;
    [SerializeField] private float spriteChangePause = .05f;
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
        if(withLoadingScreen){
            StartCoroutine(RotateGlenn());
            StartCoroutine(LoadAsync(sceneName));
        }
        else SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        isLoading = true;
    }

    public void LoadCurrentScene(){
        if(currentScene != null)
            SceneManager.UnloadSceneAsync(currentScene);
        currentScene = LevelToLoadString();
        StartCoroutine(LoadAsync(currentScene));
        StartCoroutine(RotateGlenn());
        isLoading = true;
    }

    public void LoadNewScene(){
        if(currentScene != null)
            SceneManager.UnloadSceneAsync(currentScene);
        levelToLoad = 0;
        PlayerPrefs.SetInt("currentLevelID", levelToLoad);
        currentScene = LevelToLoadString();
            StartCoroutine(LoadAsync(currentScene));
        StartCoroutine(RotateGlenn());
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
        while(!op.isDone){
            float progress = Mathf.Clamp01(op.progress / .9f);
            loadText.text = ((int)(progress * 100f)).ToString() + "%";
            yield return null;
        }
        //panel.SetActive(false);
        loadText.text = "Press any key.";
    }

    IEnumerator RotateGlenn(){
        int counter = 0;
        float lastSpriteUpdate = Time.time;
        while(true){
            if(Time.time >= lastSpriteUpdate + spriteChangePause){
                lastSpriteUpdate += spriteChangePause;
                counter++;
                if(counter > glennSprites.Count) counter = 0;
                glennSprite.sprite = glennSprites[counter];
            }
            yield return true;
        }
    }
    public void LevelCompleted(){
        SetLevelToLoad();
        PlayerPrefs.SetInt("currentLevelID", levelToLoad);
        LoadCurrentScene();
    }
    string LevelToLoadString(){
        switch(levelToLoad){
            case 0: return "Level1";
            case 1: return "Interlude1";
            case 2: return "Level2";
            case 3: return "MenuScene";
        }
        return "oh fuck";
    }
    void SetLevelToLoad()
    {
        if (currentScene == "Level1") levelToLoad = 1;
        else if (currentScene == "Interlude1") levelToLoad = 2;
        else if (currentScene == "Level2") levelToLoad = 3;
    }
}
