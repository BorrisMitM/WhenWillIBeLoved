using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Node activeNode;
    List<Node> nodes;

    void Start()
    {
        nodes = GetComponentsInChildren<Node>().ToList<Node>();
    }

    public void CheckForWin()
    {
        foreach (Node node in nodes)
        {
            if (node.connectedNodes.Count != node.maxConnections) return;

        }

        FindObjectOfType<PuzzleWindow>().PuzzleReady();
    }

    [ContextMenu("Restart")]
    public void Restart()
    {
        foreach(Node node in nodes)
        {
            LineRenderer[] lineRenderers = node.GetComponentsInChildren<LineRenderer>();

            for (int i = lineRenderers.Length-1; i >= 0; i--)
            {
                Destroy(lineRenderers[i].gameObject);

            }

            node.connectedNodes = new List<Node>();
            node.UpdateText();

        }
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
