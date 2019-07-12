using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IntroCutscene : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject scrollingDing;
    [SerializeField] private TextMeshProUGUI textLabel;
    [SerializeField] private float characterPerSecond;
    [SerializeField] private float refreshRate = 0.03f;
    [SerializeField] private List<string> introTexts;
    [SerializeField] private float fadeOutTimePanel = 2f;
    private bool isScrolling = false;
    private int currentText = 0;
    // Start is called before the first frame update
    void Awake()
    {
        NewText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (isScrolling)
            {
                StopAllCoroutines();
                EndScroll();
            }
            else
            {
                if(currentText >= introTexts.Count)
                {
                    textLabel.text = "";
                    Destroy(scrollingDing);
                    StartCoroutine(FadeOut());
                }
                NewText();
            }
        }
    }

    private void EndScroll()
    {
        textLabel.maxVisibleCharacters = textLabel.textInfo.characterCount;
        isScrolling = false;
        scrollingDing.SetActive(true);
    }

    void NewText()
    {
        textLabel.text = introTexts[currentText];
        currentText++;
        StartCoroutine(ScrollingTypeWriter());
        scrollingDing.SetActive(false);
    }

    IEnumerator ScrollingTypeWriter()
    {
        if (isScrolling) yield break;
        isScrolling = true;
        textLabel.ForceMeshUpdate();
        int currentChar = 0;
        while (currentChar < textLabel.textInfo.characterCount)
        {
            if (!isScrolling) yield break;
            textLabel.maxVisibleCharacters = currentChar;
            currentChar += Mathf.CeilToInt(characterPerSecond * refreshRate);
            yield return new WaitForSeconds(0.03f);
        }
        EndScroll();
    }

    IEnumerator FadeOut()
    {
        float startTime = Time.time;
        Image img = panel.GetComponent<Image>();
        while(Time.time < startTime + fadeOutTimePanel)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, Mathf.Lerp(1f, 0f, (Time.time - startTime) / fadeOutTimePanel));
            yield return null;
        }
        Destroy(gameObject);
    }
}
