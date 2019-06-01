using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextStuff : MonoBehaviour
{
    [SerializeField] private string myText = "Empty Text...........................................................................................";
    [SerializeField] private int charsPerSecond = 5;
    private float nextChar = 0;
    private TextMeshProUGUI tmpGUI;
    private int shownLength = 0;
    public bool active = true;
    public string recordedTemp;
    [SerializeField]
    private GameObject textField;

    // Start is called before the first frame update
    void Start()
    {
        tmpGUI = GetComponent<TextMeshProUGUI>();
        tmpGUI.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(nextChar <= Time.time && active){
            nextChar += 1f / charsPerSecond;
            shownLength++;
            if(shownLength >= myText.Length) {
                active = false;
                return;
            }
            tmpGUI.text = myText.Substring(0, shownLength);
            if(Input.GetKey(KeyCode.Space)){
                recordedTemp += myText[shownLength];
            }
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            Recording myRecord = new Recording(recordedTemp);
            recordedTemp = "";
            GameObject currentTextField = Instantiate(textField);
            currentTextField.GetComponent<TextMeshProUGUI>().text = myRecord.recordedText;
            currentTextField.transform.SetParent(transform.parent);
            currentTextField.transform.localPosition = new Vector2(Random.Range(-200, 200), Random.Range(-140, 140));
        }
    }
}

public class Recording{
    static int recordingAmount;
    int ID;
    public string recordedText;
    public Recording(string recordedText){
        recordingAmount++;
        ID = recordingAmount;
        this.recordedText = recordedText;
    }
}
