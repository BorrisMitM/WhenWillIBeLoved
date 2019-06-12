using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConnectionPuzzle : MonoBehaviour
{
    
    public List<GameObject> dots;

    public LineRenderer LineRenderer;

    
    void Start()
    {
        GetReferences();
    }

    void GetReferences()
    {
        //dots = GameObject.FindGameObjectsOfType("Node");

        LineRenderer = gameObject.GetComponent<LineRenderer>();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
