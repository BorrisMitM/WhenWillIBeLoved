using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayButton : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(GameManager.instance.LoadCurrentScene);
    }
    public void DisableMenu(){
        menu.SetActive(false);
    }
}
