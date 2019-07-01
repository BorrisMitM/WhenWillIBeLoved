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
    [SerializeField] private float afterPuzzleFade;
    private PuzzleItem pi;
    [ContextMenu("Activate")]
    public void Activate(List<GameObject> puzzlePrefabs, List<string> _puzzleTexts, PuzzleItem _pi)
    {
        //puzzleIndex = 0;
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
        textActive = false;
        Destroy(puzzle);
    }

    public void PuzzleReady()
    {
        // foreach(SpriteRenderer sprite in puzzle.GetComponentsInChildren<SpriteRenderer>()){
        //     StartCoroutine(FadeAndSetActive(sprite, puzzle, 1f, 0f, true));
        // }
        StartCoroutine(FadeOutScale(puzzle.transform, 1f));
        if(puzzleTexts.Count > puzzleIndex){        
            //textField.SetActive(true);
            StartCoroutine(FadeAndSetActive(textField.GetComponent<SpriteRenderer>(), textField, 1f, .3f));
            textField.GetComponentInChildren<TextMeshProUGUI>().text = puzzleTexts[puzzleIndex];
        }
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
                puzzleIndex = 0;
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

    private IEnumerator FadeAndSetActive(SpriteRenderer sprite, GameObject go, float duration, float targetAlpha = 0f, bool destroy = false){
        if(!go.activeSelf) go.SetActive(true); 
        float startTime = Time.time;
        Color fadeoutColor = new Color(sprite.color.r,sprite.color.g,sprite.color.b, targetAlpha);
        Color startColor = sprite.color;
        while(Time.time < startTime + duration){
            sprite.color = Color.Lerp(startColor, fadeoutColor, (Time.time -startTime) / duration );
            yield return null;
        }
        sprite.color = fadeoutColor;
        if(destroy) Destroy(go);
        else if(go.activeSelf && targetAlpha == 0f) go.SetActive(false);
    }
    private IEnumerator FadeOutScale(Transform trans, float duration){
        float startTime = Time.time;
        while(Time.time < startTime + duration){
            trans.localScale = Vector3.Lerp(trans.localScale, Vector3.zero, (Time.time - startTime) / duration);
            yield return null;
        }
        Destroy(trans.gameObject);
    }
}
