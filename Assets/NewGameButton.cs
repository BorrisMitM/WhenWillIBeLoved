using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewGameButton : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(GameManager.instance.LoadNewScene);
    }
}
