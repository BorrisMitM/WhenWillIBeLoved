using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
public class KeyManager : MonoBehaviour
{
    List<KeyPart> keyParts;
    // Start is called before the first frame update
    void OnEnable()
    {
        keyParts = new List<KeyPart>();
        keyParts = GetComponentsInChildren<KeyPart>().ToList<KeyPart>();
    }

    public void CheckForWin()
    {
        bool win = true;
        foreach(KeyPart keyPart in keyParts)
        {
            if (!keyPart.wantedStates.Contains(keyPart.myState))
            {
                Debug.Log(keyPart.gameObject.name);
                win = false;
                break;
            }
        }
        if (win)
        {
            FindObjectOfType<PuzzleWindow>().textField.GetComponentInChildren<TextMeshProUGUI>().fontSize = 10f;
            FindObjectOfType<PuzzleWindow>().PuzzleReady();
        }
    }
}
