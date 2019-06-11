using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPuzzleManager : MonoBehaviour
{
    public GameObject backgroundTile;
    public int xSize, ySize;
    public float tileSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [ExecuteInEditMode]
    [ContextMenu("SpawnField")]
    public void SpawnField()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Instantiate(backgroundTile, new Vector3(x * tileSize - (xSize / 2f - .5f) * tileSize, y * tileSize - (ySize / 2f - .5f) * tileSize, 0), Quaternion.identity, transform);
            }
        }
        SpawnWalls();
    }

    [ExecuteInEditMode]
    [ContextMenu("SpawnWalls")]
    public void SpawnWalls()
    {
        BoxCollider2D wall = gameObject.AddComponent<BoxCollider2D>();
        wall.offset = new Vector2(xSize / 2f + 0.25f, 0f);
        wall.size = new Vector2(.5f, ySize);

        wall = gameObject.AddComponent<BoxCollider2D>();
        wall.offset = new Vector2(- xSize / 2f - 0.25f, 0f);
        wall.size = new Vector2(.5f, ySize);

        wall = gameObject.AddComponent<BoxCollider2D>();
        wall.offset = new Vector2(0f, ySize / 2f + 0.25f);
        wall.size = new Vector2(xSize, .5f);

        wall = gameObject.AddComponent<BoxCollider2D>();
        wall.offset = new Vector2(0f, -ySize / 2f - 0.25f);
        wall.size = new Vector2(xSize, .5f);
    }
}
