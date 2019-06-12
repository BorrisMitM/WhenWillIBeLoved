using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Node : MonoBehaviour
{

    public List<Node> nodes;
    public List<Node> connectedNodes;

    public GameObject LineRendererPrefab;

    LineRenderer lineRenderer;

    PuzzleManager puzzleManager;

    public int maxConnections;

    private TextMeshProUGUI counterText;




    void Start()
    {
        GetReferences();
        UpdateText();
    }


    void GetReferences()
    {

        counterText = GetComponentInChildren<TextMeshProUGUI>();
        puzzleManager = FindObjectOfType<PuzzleManager>();

    }

    void UpdateText()
    {
        counterText.text = (maxConnections - connectedNodes.Count).ToString();
    }

    void Update()
    {
        if (puzzleManager.activeNode == this)
        {
            lineRenderer.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        }
    }

    private void OnMouseDown()
    {
        if (puzzleManager.activeNode != null)
        {
            if (puzzleManager.activeNode == this || connectedNodes.Contains(puzzleManager.activeNode) || connectedNodes.Count >= maxConnections)
            {
                return;
            }

           puzzleManager.activeNode.lineRenderer.SetPosition(0, puzzleManager.activeNode.transform.position);
           puzzleManager.activeNode.lineRenderer.SetPosition(1, transform.position);

            connectedNodes.Add(puzzleManager.activeNode);
            puzzleManager.activeNode.connectedNodes.Add(this);

            UpdateText();
            puzzleManager.activeNode.UpdateText();

            puzzleManager.CheckForWin();



            puzzleManager.activeNode = null;
        }

        else
        {
            if (connectedNodes.Count < maxConnections)
            {
                puzzleManager.activeNode = this;

                GameObject thisLineRenderer = Instantiate(LineRendererPrefab, transform);
                lineRenderer = thisLineRenderer.GetComponent<LineRenderer>();
                lineRenderer.SetPosition(0, puzzleManager.activeNode.transform.position);

            }
        }
    }
    

}
