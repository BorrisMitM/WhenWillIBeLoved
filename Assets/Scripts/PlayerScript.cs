using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed = 5f;

    public GameObject movementCamera;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
            float verticalMovement = Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

            Vector3 movementDirection = new Vector3(horizontalMovement, verticalMovement, 0);
            gameObject.transform.Translate(movementDirection);
        }

        if (Input.GetKey(KeyCode.W))
        {
            
        }
    }
}
