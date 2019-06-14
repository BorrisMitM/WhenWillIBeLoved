using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;

    public float moveOffset;

    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.puzzleActive)
        {
            isoRenderer.SetDirection(new Vector2(0, 0));
            return;
        }

        Vector3 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal")*moveOffset;
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputVector = new Vector3(horizontalInput, verticalInput, 0);
        inputVector = Vector3.ClampMagnitude(inputVector, 1);
        Vector3 movement = inputVector * movementSpeed;
        Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);
    }
}
