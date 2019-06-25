using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Articy.Side_Effects;
using Articy.Unity;
public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    string currentScene;

    public bool puzzleActive;
    public bool nextDialogUnlocked = true;


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
