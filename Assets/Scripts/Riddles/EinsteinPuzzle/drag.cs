using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    Vector3 dist;
    float posX;
    float posY;
   

    public enum InformationType { name, trousers, age, instrument }
    public InformationType infoType;
    public string thisInformation = "";
    public static GameObject activeGO;
    private List<snap> currentSnap;
    private Vector3 startPosition;
    private void Start()
    {
        currentSnap = new List<snap>();
        startPosition = transform.position;
    }
    private void OnMouseDown()
    {
        if (currentSnap.Count > 0) currentSnap[0].currentInfo = "";
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        activeGO = gameObject;
    }
    
    private void OnMouseDrag()
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY,
                                    dist.z);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
    }

    private void OnMouseUp()
    {
        if (currentSnap.Count == 0)
        {
            transform.position = startPosition;
        }
        else
        {
            float minDistance = 100f;
            int minID = 0;
            for (int i = 0; i < currentSnap.Count; i++)
            {
                float thisDistance = (transform.position - currentSnap[i].transform.position).magnitude;
                if ( thisDistance < minDistance)
                {
                    minDistance = thisDistance;
                    minID = i;
                }
            }
            if (!currentSnap[minID].DO()) transform.position = startPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        snap thisSnap = collision.gameObject.GetComponent<snap>();
        if (thisSnap)
        {
            currentSnap.Add(thisSnap);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        snap thisSnap = collision.gameObject.GetComponent<snap>();
        if (thisSnap)
        {
            if (currentSnap.Contains(thisSnap)) currentSnap.Remove(thisSnap);
        }
    }
}
