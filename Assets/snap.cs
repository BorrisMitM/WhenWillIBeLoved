using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snap : MonoBehaviour
{
    public drag.InformationType infoType;
    public string wantedInfo;
    public string currentInfo;
    static List<snap> allsnaps;
    private void Awake()
    {
        if (allsnaps == null) allsnaps = new List<snap>();
    }
    private void Start()
    {
        allsnaps.Add(this);
    }
    public bool DO()
    {
        if (drag.activeGO.GetComponent<drag>().infoType == infoType)
        {
            Vector3 currentPos = transform.position;
            drag.activeGO.transform.position = new Vector3(currentPos.x,
                                                           currentPos.y,
                                                           -0.5f);

            currentInfo = drag.activeGO.GetComponent<drag>().thisInformation;
            drag.activeGO = null;

            bool done = true;
            foreach (snap s in allsnaps)
            {
                if (s.wantedInfo != s.currentInfo)
                {
                    done = false;
                    break;
                }
            }

            if (done)
            {
                FindObjectOfType<PuzzleWindow>().PuzzleReady();
            }

            return true;
        }
        drag.activeGO = null;
        return false;
    }
}
