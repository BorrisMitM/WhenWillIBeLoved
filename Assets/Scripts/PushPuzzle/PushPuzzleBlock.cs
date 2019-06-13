﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PushPuzzleBlock : MonoBehaviour
{
    [SerializeField] private bool vertical = false;
    private Rigidbody2D rb;
    public bool isSolutionBlock = false;
    [SerializeField] private int length = 1;
    public int touchingEndTiles = 0;
    private float mouseOffset;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnMouseDown()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        mouseOffset = vertical ? transform.position.y - Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition).y : 
                                 transform.position.x - Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition).x;
    }
    private void OnMouseUp()
    {
        rb.bodyType = RigidbodyType2D.Static;
        Align();
    }
    private void OnMouseDrag()
    {
        if (vertical)
            rb.MovePosition(new Vector2(transform.position.x, Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition).y + mouseOffset));
        else
            rb.MovePosition(new Vector2(Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition).x + mouseOffset, transform.position.y));
    }
    public void AddTouchingEndTile()
    {
        touchingEndTiles++;
        if (touchingEndTiles >= length)
            GetComponentInParent<ChangeGlobalVariable>().SetBool(true);
    }
    [ExecuteInEditMode]
    [ContextMenu("AdjustSize")]
    public void AdjustSize()
    {
        if (vertical)
            transform.localScale = new Vector3(1f, length, 1f);
        else
            transform.localScale = new Vector3(length, 1f, 1f);
        if (isSolutionBlock) GetComponent<SpriteRenderer>().color = Color.cyan;
    }
    [ExecuteInEditMode]
    [ContextMenu("Align")]
    public void Align()
    {
        if (vertical)
        {
            float yPos;
            if (length % 2 == 1)
                yPos = Mathf.Round(transform.position.y - 0.5f) + 0.5f;
            else yPos = Mathf.Round(transform.position.y);
            transform.position = new Vector3(Mathf.Round(transform.position.x - 0.5f) + 0.5f, yPos, -2f);
        }
        else
        {
            float xPos;
            if (length % 2 == 1)
                xPos = Mathf.Round(transform.position.x - 0.5f) + 0.5f;
            else xPos = Mathf.Round(transform.position.x);
            transform.position = new Vector3(xPos, Mathf.Round(transform.position.y - 0.5f) + 0.5f, -2f);
        }
    }
}