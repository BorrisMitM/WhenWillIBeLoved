using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleWindow : MonoBehaviour
{

    GameObject puzzle;

    [ContextMenu("Activate")]
    public void Activate(GameObject puzzlePrefab)
    {
        gameObject.transform.GetChild(0).position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
        transform.GetChild(0).gameObject.SetActive(true);
        GameManager.instance.puzzleActive = true;
        Instantiate(puzzlePrefab, new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z), Quaternion.identity, transform);

    }

    [ContextMenu("Deactivate")]
    public void Deactivate()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        GameManager.instance.puzzleActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
