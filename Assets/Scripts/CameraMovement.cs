using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 startingPosition;
    public Transform targetToFollow;
    private Vector3 targetPosition;

    public float moveSpeed;
    public float smoothTime;

    


    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(targetToFollow != null)
        {
            targetPosition = new Vector3(targetToFollow.transform.position.x, targetToFollow.transform.position.y, transform.position.z);
            Vector3 velocity = (targetPosition - startingPosition) * moveSpeed;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

            
        }
    }
}
