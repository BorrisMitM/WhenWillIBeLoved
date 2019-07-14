using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyState { original, up, turned, toriginal, tup, tturned};
public class KeyPart : MonoBehaviour
{
    [SerializeField] private Sprite original;
    [SerializeField] private Sprite up;
    [SerializeField] private Sprite turned;
    [SerializeField] private Sprite sideView;
    [SerializeField] private Sprite toriginal;
    [SerializeField] private Sprite tup;
    [SerializeField] private Sprite tturned;
    [SerializeField] float originalRotation = 0f;
    [SerializeField] int dir = 1;
    [SerializeField] int hingeDirection = 1;
    int parentLayer = 0;
    public List<DependingHinge> dependingOn;
    private KeyState myState = KeyState.original;
    private SpriteRenderer sr;
    private bool isSideView;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetSprite();
    }

    private void OnMouseDown()
    {
        if(myState == KeyState.original)
        {
            myState = KeyState.up;
        }
        else if (myState == KeyState.up)
        {
            myState = KeyState.turned;
        }
        else if (myState == KeyState.turned)
        {
            myState = KeyState.original;
        }
        else if (myState == KeyState.toriginal)
        {
            myState = KeyState.tup;
        }
        else if (myState == KeyState.tup)
        {
            myState = KeyState.tturned;
        }
        else if (myState == KeyState.tturned)
        {
            myState = KeyState.toriginal;
        }
        else if (isSideView)
        {
        }
        SetSprite();
        if (dependingOn != null)
        foreach(DependingHinge dh in dependingOn)
        {
            if (myState == KeyState.original) dh.keyPart.SetDepending(myState, dh.OhingePosition.position, 0);
            else if (myState == KeyState.up) dh.keyPart.SetDepending(myState, dh.UhingePosition.position, hingeDirection);
            else if (myState == KeyState.turned) dh.keyPart.SetDepending(myState, dh.ThingePosition.position, hingeDirection);
        }
    }

    public void SetDepending(KeyState turnCode, Vector3 newPosition, int _parentLayer)
    {
        parentLayer = _parentLayer;
        if (turnCode == KeyState.original)
        {
            if (myState == KeyState.toriginal) myState = KeyState.original;
            else if (myState == KeyState.tup) myState = KeyState.up;
            else if (myState == KeyState.tturned) myState = KeyState.turned;
        }
        else if (turnCode == KeyState.turned)
        {
            isSideView = false;
            if (myState == KeyState.original) myState = KeyState.toriginal;
            else if (myState == KeyState.original) myState = KeyState.tup;
            else if (myState == KeyState.original) myState = KeyState.tturned;
        }
        isSideView = turnCode == KeyState.up;

        transform.position = newPosition;
        SetSprite();
    }
    void SetSprite()
    {
        Destroy(GetComponent<BoxCollider2D>());
        switch (myState)
        {
            case KeyState.original: if (original != null) sr.sprite = original; sr.sortingOrder = parentLayer; break;
            case KeyState.up: if (up != null) sr.sprite = up; sr.sortingOrder = parentLayer; break;
            case KeyState.turned: if (turned != null) sr.sprite = turned; sr.sortingOrder = parentLayer + hingeDirection; break;
            case KeyState.toriginal: if (toriginal != null) sr.sprite = toriginal; sr.sortingOrder = parentLayer; break;
            case KeyState.tup: if (tup != null) sr.sprite = tup; sr.sortingOrder = parentLayer; break;
            case KeyState.tturned: if (tturned != null) sr.sprite = tturned; sr.sortingOrder = parentLayer - hingeDirection; break;
        }
        if(isSideView)
        {
            if (sideView != null)
                sr.sprite = sideView;
            if (myState == KeyState.original) transform.rotation = Quaternion.Euler(0f, 0f, originalRotation);
            else if (myState == KeyState.up) transform.rotation = Quaternion.Euler(0f, 0f, originalRotation + 90f * dir);
            else if (myState == KeyState.turned) transform.rotation = Quaternion.Euler(0f, 0f, originalRotation + 180f * dir);
        }
        else transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        gameObject.AddComponent<BoxCollider2D>();
    }
    [System.Serializable]
    public struct DependingHinge
    {
        public KeyPart keyPart;
        public Transform OhingePosition;
        public Transform UhingePosition;
        public Transform ThingePosition;
    }

}
