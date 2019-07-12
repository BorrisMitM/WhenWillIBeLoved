using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPuzzleTile : MonoBehaviour
{
    [SerializeField] private bool isSolutionTile = false;
    private void Start()
    {
        if (isSolutionTile) GetComponent<SpriteRenderer>().color = Color.green;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PushPuzzleBlock block = collision.GetComponent<PushPuzzleBlock>();
        if(block != null)
        {
            if (block.isSolutionBlock && isSolutionTile)
                block.AddTouchingEndTile();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PushPuzzleBlock block = collision.GetComponent<PushPuzzleBlock>();
        if (block != null)
        {
            if (block.isSolutionBlock && isSolutionTile)
                block.touchingEndTiles--;
        }
    }
}
