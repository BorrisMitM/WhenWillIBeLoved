using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 startingPosition;
    public Transform targetToFollow;
    private Vector3 targetPosition;

    public CameraShake cameraShaker;

    public float moveSpeed;
    public float smoothTime;
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;
    [SerializeField] private float topBorder;
    [SerializeField] private float botBorder;

    

    Vector3 velocity;
    void Start()
    {
        startingPosition = transform.position;
        cameraShaker = GetComponentInChildren<CameraShake>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(cameraShaker.ShakeCamera(.15f, .4f));
        }
    }

    void LateUpdate()
    {
        if (GameManager.instance.puzzleActive) return;

        if(targetToFollow != null)
        {
            targetPosition = new Vector3(Mathf.Clamp(targetToFollow.transform.position.x, leftBorder, rightBorder), 
                                         Mathf.Clamp(targetToFollow.transform.position.y, botBorder, topBorder), 
                                         transform.position.z);
                                         
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
