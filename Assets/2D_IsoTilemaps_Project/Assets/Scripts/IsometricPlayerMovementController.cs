﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    [HideInInspector]
    public IsometricCharacterRenderer isoRenderer;
    [SerializeField] private bool turnMovement = true;
    public float moveOffset;

    [SerializeField]
    AudioClip[] steps;
    [SerializeField]
    AudioSource currentAudio;
    bool needNewSound = true;
   


    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();

        currentAudio = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.puzzleActive || GameManager.instance.disableMovement)
        {
            isoRenderer.SetDirection(new Vector2(0, 0));
            return;
        }

        Vector3 currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputVector = new Vector3(horizontalInput, verticalInput, 0);
        if(turnMovement)
            inputVector = inputVector.Rotate(-45f, Vector3.forward);
        inputVector.x *= moveOffset;
        inputVector = Vector3.ClampMagnitude(inputVector, 1);
        Vector3 movement = inputVector * movementSpeed;
        Vector3 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbody.MovePosition(newPos);


        if ( (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 ) && needNewSound)
        {
            currentAudio.clip = steps[Random.Range(0, steps.Length)];
            currentAudio.volume = 0.5f;
            currentAudio.Play();
            needNewSound = false;
            StartCoroutine(WaitForFootStep());

            
        }
    }

    IEnumerator WaitForFootStep()
    {
        yield return new WaitForSeconds(0.3f);
        needNewSound = true;
    }
}
