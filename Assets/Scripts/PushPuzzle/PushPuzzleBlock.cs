using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PushPuzzleBlock : MonoBehaviour
{
    [SerializeField] private bool vertical = false;
    private static int snippetCount = 0;
    private Rigidbody2D rb;
    public bool isSolutionBlock = false;
    [SerializeField] private int length = 1;
    public int touchingEndTiles = 0;
    private float mouseOffset;
    private PushPuzzleManager manager;
    [SerializeField] private Sprite[] threeLong;
    [SerializeField] private Sprite[] twoLong;

    [SerializeField] private Sprite solutionSprite;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(isSolutionBlock)
            manager = GetComponentInParent<PushPuzzleManager>();
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
        if (touchingEndTiles >= length){
            snippetCount++;
            if(snippetCount >= manager.maxSnippetCount) {
                FindObjectOfType<RiddleTextPanel>().EnablePanel(manager.PostPuzzleText);
                GameManager.instance.nextDialogUnlocked = true;
            }
            FindObjectOfType<ArticyManager>().UnlockNextDialog();
            //GetComponentInParent<ChangeGlobalVariable>().SetBool(true);
        }
    }
    [ExecuteInEditMode]
    [ContextMenu("AdjustSize")]
    public void AdjustSize()
    {
        // if (vertical)
        //     transform.localScale = new Vector3(1f, length, 1f);
        // else
        //     transform.localScale = new Vector3(length, 1f, 1f);
        // if (isSolutionBlock) GetComponent<SpriteRenderer>().color = Color.cyan;
        if(!vertical || isSolutionBlock){
            transform.rotation = Quaternion.Euler(0f,0f, 90f);
        }
        if(isSolutionBlock)
            GetComponent<BoxCollider2D>().size = new Vector2( length * 0.95f,  0.95f);
        else
            GetComponent<BoxCollider2D>().size = new Vector2( 0.95f, length * 0.95f);

        if(isSolutionBlock) GetComponent<SpriteRenderer>().sprite = solutionSprite;
        else if(length == 2) GetComponent<SpriteRenderer>().sprite = twoLong[Random.Range(0, twoLong.Length)];
        else if(length == 3) GetComponent<SpriteRenderer>().sprite = threeLong[Random.Range(0, threeLong.Length)];
        transform.localScale = Vector3.one;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    [ExecuteInEditMode]
    [ContextMenu("Align")]
    public void Align()
    {
        if (vertical)
        {
            float yPos;
            if (length % 2 == 1)
                yPos = Mathf.Round(transform.localPosition.y - 0.5f) + 0.5f;
            else yPos = Mathf.Round(transform.localPosition.y);
            transform.localPosition = new Vector3(Mathf.Round(transform.localPosition.x - 0.5f) + 0.5f, yPos, -2f);
        }
        else
        {
            float xPos;
            if (length % 2 == 1)
                xPos = Mathf.Round(transform.localPosition.x - 0.5f) + 0.5f;
            else xPos = Mathf.Round(transform.localPosition.x);
            transform.localPosition = new Vector3(xPos, Mathf.Round(transform.localPosition.y - 0.5f) + 0.5f, -2f);
        }
    }

}
