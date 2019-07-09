using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(GameManager.instance.LoadCurrentScene);
    }
}
