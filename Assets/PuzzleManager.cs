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

        Debug.Log("Win!");
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
