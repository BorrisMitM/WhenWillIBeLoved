using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Articy.Unity;
using Articy.Unity.Interfaces;
using Articy.Unity.Utils;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ArticyManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
    [Header("UI")]
	// a prefab used to instantiate a branch
	public GameObject branchPrefab;
	// the main text label, used to show the text of the current paused on node
	public TextMeshProUGUI textLabel;

	// the ui target for our vertical list of branch buttons
	public RectTransform branchLayoutPanel;

    public GameObject scrollingDing;
    public GameObject panel;
    private bool isActive = false;

	// the preview image ui element. A simple 64x64 image that will show the articy preview image or speaker, depending on the current pause.
	public Image portrait;

	[Header("Scrolling")]
	[SerializeField]
	private int charactersPerSecond = 20;

    public enum ScrollingType {TypeWriter, FadeIn}
    [SerializeField] private ScrollingType scrollingType;
    ArticyFlowPlayer flowPlayer;
    Branch singleBranch;

	List<Branch> branches;
    [Header("other stuff")]
    public bool buttonPressed = false;
    [SerializeField] private List<ArticyRef> dialogs;
    private int dialogCount = 0;
	bool isScrolling;
    bool scrollingStart = false;

    private Sprite lastSprite;

    public delegate void DialogEnded();
    public static event DialogEnded OnDialogEnded;

    public void StartDialog(){
        if(isActive) return;
        panel.SetActive(true);
        GameManager.instance.puzzleActive = true;
        GameManager.instance.nextDialogUnlocked = false;
        isActive = true;
        StartScroll();
    }

    public void EndDialog(){
        panel.SetActive(false);
        GameManager.instance.puzzleActive = false;
        isActive = false;
        OnDialogEnded();
    }
    void Start()
    {
		branches = new List<Branch>();
        //flowPlayer = GetComponent<ArticyFlowPlayer>();
        UnlockNextDialog();
        if (flowPlayer != null && flowPlayer.StartOn == null)
			textLabel.text = "<color=green>No object selected in the flow player. Navigate to the ArticyflowPlayer and choose a StartOn node.</color>";
    }
    private void LateUpdate() {
        if(Input.GetButtonDown("Interact") && !buttonPressed && isActive){
			if(isScrolling){
				EndScroll();
			}
			else if(singleBranch != null){
				Branch thisBranch = singleBranch;
				flowPlayer.Play(singleBranch);
				if(singleBranch == thisBranch) singleBranch = null;
			}
        }
        if(buttonPressed) buttonPressed = false;
    }
    #region DirectArticyStuff
    // This is called everytime the flow player reaches and object of interest.
    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
		if(aObject != null)
		{
			IArticyObject articyObj = aObject as IArticyObject;	
		}
		// To show text in the ui of the current node
		// we just check if it has a text property by using the object property interfaces, 
        // if it has the property we use it to show the text in our main text label.
		var modelWithText = aObject as IObjectWithText;
		if (modelWithText != null)
			textLabel.text = modelWithText.Text;
		else
            EndDialog();
            //textLabel.text = string.Empty;

		// this will make sure that we find a proper preview image to show in our ui.
		ExtractCurrentPausePreviewImage(aObject);
    }
    
	// called everytime the flow player encounteres multiple branches, or paused on a node and want to tell us how to continue.
    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        // we clear all old branch buttons
		ClearAllBranches();
        //show layout panel only when branches are available
        if(aBranches.Count == 1 && aBranches[0].IsValid){
             branchLayoutPanel.gameObject.SetActive(false);
             singleBranch = aBranches[0];
			 branches = new List<Branch>();
             if(textLabel.text.Length <= 0) flowPlayer.Play(singleBranch);
        }else{
            branches = new List<Branch>();
            singleBranch = null;
			foreach(Branch branch in aBranches)
				branches.Add(branch);
        }
        if (isActive)
            StartScroll();

    }
    
	// convenience method to clear everything underneath our branch layout panel, this should only be our dynamically created branch buttons.
	private void ClearAllBranches()
	{
		foreach (Transform child in branchLayoutPanel)
			Destroy(child.gameObject);
	}

    private void ExtractCurrentPausePreviewImage(IFlowObject aObject)
	{
		IAsset articyAsset = null;

		//portrait.sprite = null;

		// to figure out which asset we could show in our preview, we first try to see if it is an object with a speaker
		IObjectWithSpeaker dlgSpeaker = aObject as IObjectWithSpeaker;
		if (dlgSpeaker != null)
		{
			// if we have a speaker, we extract it, because now we have to check if it has a preview image.
			ArticyObject speaker = dlgSpeaker.Speaker;
			if (speaker != null)
			{
				IObjectWithPreviewImage speakerWithPreviewImage = speaker as IObjectWithPreviewImage;
				if (speakerWithPreviewImage != null)
				{
					// our speaker has the property for preview image and we assign it to our asset.
					articyAsset = speakerWithPreviewImage.PreviewImage.Asset;
				}
			}
		}

		// if we have no asset until now, we could try to check if the target itself has a preview image.
		if (articyAsset == null)
		{
			var objectWithPreviewImage = aObject as IObjectWithPreviewImage;
			if (objectWithPreviewImage != null)
			{
				articyAsset = objectWithPreviewImage.PreviewImage.Asset;
			}
		}

		// and if we have an asset at this point, we load it as a sprite and show it in our ui image.
		if (articyAsset != null)
		{
			portrait.sprite = articyAsset.LoadAssetAsSprite();
		}
	}

    private void EnableContextMenu()
    {
        if (singleBranch != null) return;
        ClearAllBranches();
        if (!branchLayoutPanel.gameObject.activeSelf) branchLayoutPanel.gameObject.SetActive(true);
        List<Branch> alreadyInstantiated = new List<Branch>();
        // for every branch provided by the flow player, we will create a button in our vertical list
        foreach (var branch in branches)
        {
            // if the branch is invalid, because a script evaluated to false, we don't create a button.
            if (!branch.IsValid || alreadyInstantiated.Contains(branch)) continue;
            alreadyInstantiated.Add(branch);
            // we create a our button prefab and parent it to our vertical list
            var btn = Instantiate(branchPrefab);
            var rect = btn.GetComponent<RectTransform>();
            rect.SetParent(branchLayoutPanel, false);

            // here we make sure to get the Branch component from our button, either by referencing an already existing one, or by adding it.
            var branchBtn = btn.GetComponent<ArticyChoiceButton>();
            if (branchBtn == null)
                branchBtn = btn.AddComponent<ArticyChoiceButton>();

            // this will assign the flowplayer and branch and will create a proper label for the button.
            branchBtn.AssignBranch(flowPlayer, branch);
        }
        StartCoroutine(SetEventSystem());
    }

    IEnumerator SetEventSystem(){ 
        // assign first button to eventmanager for keyboard/controller controls
        FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
        yield return null;
        FindObjectOfType<EventSystem>().SetSelectedGameObject(branchLayoutPanel.GetChild(0).gameObject);
    }
    #endregion

    #region Scrolling

    public void StartScroll()
    {
        scrollingDing.SetActive(false);
        switch (scrollingType)
        {
            case ScrollingType.TypeWriter:
                StartCoroutine(ScrollingTypeWriter());
                break;
            case ScrollingType.FadeIn:
                StartCoroutine(ScrollingFadeInAlt());
                break;
        }
    }
    

    IEnumerator ScrollingTypeWriter(){
		if(isScrolling == true) yield break;
        isScrolling = true;
        textLabel.ForceMeshUpdate();
        int currentChar = 0;
        while (currentChar < textLabel.textInfo.characterCount)
        {
            if(!isScrolling) yield break;
            textLabel.maxVisibleCharacters = currentChar;
            currentChar+= 2;
            yield return new WaitForSeconds(1f / charactersPerSecond);
        }
        EndScroll();
    }
    IEnumerator ScrollingFadeInAllt(){
		if (isScrolling == true) yield break;
        isScrolling = true;
        // Need to force the text object to be generated so we have valid data to work with right from the start.
        textLabel.ForceMeshUpdate();
        yield return null;

        TMP_TextInfo textInfo = textLabel.textInfo;
        Color32[] newVertexColors;

        for (int i = 0; i < textLabel.textInfo.characterCount; i++)
        {
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            // Set new alpha values.
            vertexColors[vertexIndex + 0].a = 0;
            vertexColors[vertexIndex + 1].a = 0;
            vertexColors[vertexIndex + 2].a = 0;
            vertexColors[vertexIndex + 3].a = 0;
        }
        textLabel.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        int currentChar = 0;
        while (currentChar < textLabel.textInfo.characterCount)
        {
            //StartCoroutine(FadeIn(currentChar));
            currentChar++;
            yield return new WaitForSeconds(1f / charactersPerSecond);
        }
        EndScroll();
    }

    private void EndScroll()
    {
        if (scrollingStart) return;
        textLabel.maxVisibleCharacters = textLabel.textInfo.characterCount;
        textLabel.ForceMeshUpdate();
        isScrolling = false;
        scrollingDing.SetActive(true);
		EnableContextMenu();
    }

    IEnumerator ScrollingFadeInAlt(){
        //yield return null;
        if (isScrolling == true) yield break;
        isScrolling = true;
        scrollingStart = true;
        // Need to force the text object to be generated so we have valid data to work with right from the start.
        textLabel.ForceMeshUpdate();
        textLabel.maxVisibleCharacters = 0;
        yield return null;
        scrollingStart = false;
        TMP_TextInfo textInfo = textLabel.textInfo;
        Color32[] newVertexColors;
        Debug.Log(textLabel.textInfo.characterCount);
        textLabel.maxVisibleCharacters = textLabel.textInfo.characterCount;
        for (int i = 0; i < textLabel.textInfo.characterCount; i++)
        {
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            // Set new alpha values.
            vertexColors[vertexIndex + 0].a = 0;
            vertexColors[vertexIndex + 1].a = 0;
            vertexColors[vertexIndex + 2].a = 0;
            vertexColors[vertexIndex + 3].a = 0;
        }
        textLabel.maxVisibleCharacters = 0;
        textLabel.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        //yield return null;
        List<int> currentlyFadingIn = new List<int>();
        int fadeInAmount = 20;
        byte fadeSteps = (byte)Mathf.Max(1, 255 / 5);
        int counter = 0;
        while(isScrolling){
            Debug.Log(currentlyFadingIn.Count);
            if(currentlyFadingIn.Count < fadeInAmount && counter < textInfo.characterInfo.Length){
                if (textInfo.characterInfo[counter].isVisible)
                    currentlyFadingIn.Add(counter);
                counter++;
                textLabel.maxVisibleCharacters = counter;
            }
            for (int it = currentlyFadingIn.Count - 1; it >= 0; it--){
                int i = currentlyFadingIn[it];
                
                // Get the index of the material used by the current character.
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                // Get the vertex colors of the mesh used by this text element (character or sprite).
                newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                // Get the index of the first vertex used by this text element.
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                // Get the current character's alpha value.
                byte alpha = (byte)Mathf.Clamp(newVertexColors[vertexIndex].a + fadeSteps, 0, 255);
                // Set new alpha values.
                newVertexColors[vertexIndex + 0].a = alpha;
                newVertexColors[vertexIndex + 1].a = alpha;
                newVertexColors[vertexIndex + 2].a = alpha;
                newVertexColors[vertexIndex + 3].a = alpha;
                if(it == 0){
                    if(alpha == 255){ 
                        currentlyFadingIn.Remove(i);
                        if(currentlyFadingIn.Count == 0) {
                            EndScroll();
                        }
                    }
                }
            }
            textLabel.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            yield return new WaitForSeconds(1f/charactersPerSecond);
        }
    }
    IEnumerator ScrollingFadeIn()
    {

        if (isScrolling == true) yield break;
        isScrolling = true;
        // Need to force the text object to be generated so we have valid data to work with right from the start.
        textLabel.ForceMeshUpdate();


        TMP_TextInfo textInfo = textLabel.textInfo;
        Color32[] newVertexColors;

        int currentCharacter = 0;
        int startingCharacterRange = currentCharacter;

        for (int i = 0; i < textLabel.textInfo.characterCount; i++)
        {
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            // Set new alpha values.
            vertexColors[vertexIndex + 0].a = 0;
            vertexColors[vertexIndex + 1].a = 0;
            vertexColors[vertexIndex + 2].a = 0;
            vertexColors[vertexIndex + 3].a = 0;
        }

        textLabel.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        while (true)
        {
            int characterCount = textInfo.characterCount;

            // Spread should not exceed the number of characters.
            byte fadeSteps = (byte)Mathf.Max(1, 255 / 10);


            for (int i = startingCharacterRange; i < currentCharacter + 1; i++)
            {
                // Skip characters that are not visible
                // if (!textInfo.characterInfo[i].isVisible) continue;

                // Get the index of the material used by the current character.
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                // Get the vertex colors of the mesh used by this text element (character or sprite).
                newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                // Get the index of the first vertex used by this text element.
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;

                // Get the current character's alpha value.
                byte alpha = (byte)Mathf.Clamp(newVertexColors[vertexIndex].a + fadeSteps, 0, 255);
                // Set new alpha values.
                newVertexColors[vertexIndex + 0].a = alpha;
                newVertexColors[vertexIndex + 1].a = alpha;
                newVertexColors[vertexIndex + 2].a = alpha;
                newVertexColors[vertexIndex + 3].a = alpha;

                if (alpha == 255)
                {
                    startingCharacterRange += 1;
                    if (startingCharacterRange == characterCount)
                    {
                        // Update mesh vertex data one last time.
                        textLabel.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                        EndScroll();
                    }
                }
            }
            // Upload the changed vertex colors to the Mesh.
            textLabel.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            if (currentCharacter + 1 < characterCount) currentCharacter += 1;
            yield return new WaitForSeconds(1f / charactersPerSecond);
        }
    }
    #endregion

    [ContextMenu("UnlockNextDialog")]
    public void UnlockNextDialog(){
        GameManager.instance.nextDialogUnlocked = true;
        ArticyFlowPlayer oldPlayer = GetComponent<ArticyFlowPlayer>();
        if(oldPlayer != null) 
            Destroy(oldPlayer);
            StartCoroutine(AddNewFlowPlayer());
    }

    IEnumerator AddNewFlowPlayer(){
        yield return null;
        flowPlayer = gameObject.AddComponent<ArticyFlowPlayer>();
        flowPlayer.startOn = dialogs[dialogCount];
        ClearAllBranches();
        dialogCount++;
    }

    public bool LastDialogPlayed(){
        return dialogCount >= dialogs.Count;
    }
}
