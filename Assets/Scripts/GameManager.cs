using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Articy.ManiacManfred;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    string currentScene;
    public Articy.ManiacManfred.GlobalVariables.GameState gameState;

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
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }
}
