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
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
