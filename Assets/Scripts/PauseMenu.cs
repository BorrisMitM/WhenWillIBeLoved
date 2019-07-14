using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }

    public void LoadMainMenu()
    {
        GameManager.instance.LoadScene("MenuScene", false);
    }
}
