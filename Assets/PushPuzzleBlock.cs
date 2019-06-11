using System.Collections;
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
    private void Start()
    {
        if (isSolutionBlock) GetComponent<SpriteRenderer>().color = Color.cyan;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnMouseDown()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    private void OnMouseUp()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }
    private void OnMouseDrag()
    {
        if (vertical)
            rb.MovePosition(new Vector2(transform.position.x, Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition).y));
        else
            rb.MovePosition(new Vector2(Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition).x,transform.position.y));
    }
    public void AddTouchingEndTile()
    {
        touchingEndTiles++;
        if (touchingEndTiles >= length)
            Debug.Log("Win");
    }
    [ExecuteInEditMode]
    [ContextMenu("AdjustSize")]
    public void AdjustSize()
    {
        if (vertical)
            transform.localScale = new Vector3(1f, length, 1f);
        else
            transform.localScale = new Vector3(length, 1f, 1f);
    }
}
