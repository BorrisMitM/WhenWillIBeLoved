using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Articy.Unity;
public class ArticyManager : MonoBehaviour, IArticyFlowPlayerCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBranchesUpdated(IList<Branch> aBranches)
    {
        throw new System.NotImplementedException();
    }

    public void OnFlowPlayerPaused(IFlowObject aObject)
    {
        throw new System.NotImplementedException();
    }
}
