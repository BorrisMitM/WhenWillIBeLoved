using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzzleWindow : MonoBehaviour
{

    GameObject puzzle;
    public GameObject textField;
    List<GameObject> activePuzzlePrefabs;
    List<string> puzzleTexts;

    private int puzzleIndex = 0;

    private bool active = false;
    private bool textActive = false;
    private PuzzleItem pi;
    [ContextMenu("Activate")]
    public void Activate(List<GameObject> puzzlePrefabs, List<string> _puzzleTexts, PuzzleItem _pi)
    {
        puzzleIndex = 0;
        gameObject.transform.GetChild(0).position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z);
        transform.GetChild(0).gameObject.SetActive(true);
        GameManager.instance.puzzleActive = true;
        puzzle = Instantiate(puzzlePrefabs[puzzleIndex], new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z), Quaternion.identity, transform.GetChild(0));

        activePuzzlePrefabs = puzzlePrefabs;
        puzzleTexts = _puzzleTexts;
        active = true;
        pi = _pi;
    }

    [ContextMenu("Deactivate")]
    public void Deactivate()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        GameManager.instance.puzzleActive = false;
        Destroy(puzzle);
    }

    public void PuzzleReady()
    {
        Destroy(puzzle);
        textField.SetActive(true);
        textField.GetComponentInChildren<TextMeshProUGUI>().text = puzzleTexts[puzzleIndex];
        textActive = true;
    }
    private void Update()
    {
        if(textActive && Input.GetButtonDown("Interact"))
        {
            textField.SetActive(false);
            textActive = false;
            puzzleIndex++;
            if (puzzleIndex >= activePuzzlePrefabs.Count)
            {
                pi.played = true;
                FindObjectOfType<ArticyManager>().UnlockNextDialog();
                Deactivate();
            }
            else puzzle = Instantiate(activePuzzlePrefabs[puzzleIndex], new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z), Quaternion.identity, transform.GetChild(0));
        }
    }
    public void Restart()
    {
        Destroy(puzzle);
        puzzle = Instantiate(activePuzzlePrefabs[puzzleIndex], new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z), Quaternion.identity, transform.GetChild(0));
    }

}
