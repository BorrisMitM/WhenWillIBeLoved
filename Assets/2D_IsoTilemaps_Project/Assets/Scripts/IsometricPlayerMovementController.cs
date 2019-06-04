using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;

    public GameObject movementCamera;


    Rigidbody rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput * 2f);
        //inputVector = Vector3.ClampMagnitude(inputVector, 1);
        Vector3 movement = inputVector * movementSpeed;
        Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(new Vector2(movement.x, movement.z));
        rbody.MovePosition(newPos);

       
    }
}
