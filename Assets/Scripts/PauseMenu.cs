using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject paused;
    bool isActive = true;

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void LoadMainMenu()
    {
        GameManager.instance.LoadScene("MenuScene", false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("pressed");
            paused.SetActive(isActive);
            isActive = !isActive;
        }
    }
}
