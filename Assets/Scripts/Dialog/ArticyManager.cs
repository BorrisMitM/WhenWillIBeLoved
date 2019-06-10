using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Articy.Unity;
using Articy.Unity.Interfaces;
using Articy.Unity.Utils;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
[RequireComponent(typeof(ArticyFlowPlayer))]
public class ArticyManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
    [Header("UI")]
	// a prefab used to instantiate a branch
	public GameObject branchPrefab;
	// the display name label, used to show the name of the current paused on node
	public TextMeshProUGUI displayNameLabel;
	// the main text label, used to show the text of the current paused on node
	public TextMeshProUGUI textLabel;

	// the ui target for our vertical list of branch buttons
	public RectTransform branchLayoutPanel;

	// the preview image ui element. A simple 64x64 image that will show the articy preview image or speaker, depending on the current pause.
	public Image portrait;
    ArticyFlowPlayer flowPlayer;
    Branch singleBranch;
    // Start is called before the first frame update
    void Start()
    {
        flowPlayer = GetComponent<ArticyFlowPlayer>();
        ClearAllBranches();
        if (flowPlayer != null && flowPlayer.StartOn == null)
			textLabel.text = "<color=green>No object selected in the flow player. Navigate to the ArticyflowPlayer and choose a StartOn node.</color>";
	
    }
    private void Update() {
        if(Input.GetButtonDown("Submit") && singleBranch != null){
            Branch thisBranch = singleBranch;
            flowPlayer.Play(singleBranch);
            if(singleBranch == thisBranch) singleBranch = null;
        }
    }

    // This is called everytime the flow player reaches and object of interest.
    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
		if(aObject != null)
		{
			IArticyObject articyObj = aObject as IArticyObject;	
		}
		// To show the displayname in the ui of the current node
		IObjectWithDisplayName modelWithDisplayName = aObject as IObjectWithDisplayName;
		if (modelWithDisplayName != null)
			displayNameLabel.text = modelWithDisplayName.DisplayName;
		else
			displayNameLabel.text = string.Empty;

		// To show text in the ui of the current node
		// we just check if it has a text property by using the object property interfaces, 
        // if it has the property we use it to show the text in our main text label.
		var modelWithText = aObject as IObjectWithText;
		if (modelWithText != null)
			textLabel.text = modelWithText.Text;
		else
			textLabel.text = string.Empty;

		// this will make sure that we find a proper preview image to show in our ui.
		ExtractCurrentPausePreviewImage(aObject);
    }
    
	// called everytime the flow player encounteres multiple branches, or paused on a node and want to tell us how to continue.
    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        // we clear all old branch buttons
		ClearAllBranches();
        //show layout panel only when branches are available
        if(aBranches.Count <= 1){
             branchLayoutPanel.gameObject.SetActive(false);
             singleBranch = aBranches[0];
             return;
        }
        else if(!branchLayoutPanel.gameObject.activeSelf) branchLayoutPanel.gameObject.SetActive(true);
		// for every branch provided by the flow player, we will create a button in our vertical list
		foreach (var branch in aBranches)
		{
			// if the branch is invalid, because a script evaluated to false, we don't create a button.
			if (!branch.IsValid) continue;
			// we create a our button prefab and parent it to our vertical list
			var btn = Instantiate(branchPrefab);
			var rect = btn.GetComponent<RectTransform>();
			rect.SetParent(branchLayoutPanel, false);

			// here we make sure to get the Branch component from our button, either by referencing an already existing one, or by adding it.
			var branchBtn = btn.GetComponent<ArticyChoiceButton>();
			if(branchBtn == null)
				branchBtn = btn.AddComponent<ArticyChoiceButton>();

			// this will assign the flowplayer and branch and will create a proper label for the button.
			branchBtn.AssignBranch(flowPlayer, branch);
		}
        StartCoroutine(SetEventSystem());
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

		portrait.sprite = null;

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

    IEnumerator SetEventSystem(){ 
        // assign first button to eventmanager for keyboard/controller controls
        FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
        yield return null;
        FindObjectOfType<EventSystem>().SetSelectedGameObject(branchLayoutPanel.GetChild(0).gameObject);
    }
}
