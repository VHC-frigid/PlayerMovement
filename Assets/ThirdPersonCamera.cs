using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //controls how fast the character looks around
    [SerializeField] private float sensitivity;

    //keep track of the player's position
    [SerializeField] private Transform playerTransform;

    //prevent the camera from rotating too far up or down
    [SerializeField] private float verticalRotationMin = 0, verticalRotationMax = 75;

    //the ideal value for the camera zoom
    [SerializeField] private float camerZoom = 20;

    //the layermask to look for when avoiding objects
    [SerializeField] private LayerMask avoidLayer;

    //track our ideal and our actual camera zoom
    private float idealCameraZoom =20;
    private float currentCameraZoom;

    //keep track of our different camera parts
    private Transform cameraTransform, boomTransform;

    //track out current rotation values
    private float currentHorizontalRotation = 0, currentVerticalRotation;


    // Start is called before the first frame update
    void Start()
    {
        //save both parts of our camera
        boomTransform = transform.GetChild(0);
        cameraTransform = boomTransform.GetChild(0);

        //set our initial rotation values
        currentHorizontalRotation = transform.localEulerAngles.y;
        currentVerticalRotation = transform.localEulerAngles.x;
    
    }

    // Update is called once per frame
    void Update()
    {
        //rotate left to right based on the mouse movement
        currentHorizontalRotation += Input.GetAxis("Mouse X") * sensitivity;

        //rotate up and down based on the mouse movement
        currentVerticalRotation -= Input.GetAxis("Mouse Y") * sensitivity;

        //clamp our vertical rotation
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, verticalRotationMin, verticalRotationMax);

        
        
        //set the new rotation values
        transform.localEulerAngles = new Vector3(0, currentHorizontalRotation);
        boomTransform.localEulerAngles = new Vector3(currentVerticalRotation, 0);

        //make the camera snap to the player
        transform.position = playerTransform.position;

        //direction from A to B = A - B
        Vector3 directionToCamera = cameraTransform.position;

        //noralise to make sure we have a magnitude of 1
        directionToCamera.Normalize();
            
        if (Physics.Raycast(transform.position, directionToCamera, out RaycastHit hit, camerZoom, avoidLayer))
        {
            currentCameraZoom = hit.distance;
        }
        else
        {
            currentCameraZoom = camerZoom;
        }
        cameraTransform.localPosition = new Vector3(0, 0, -currentCameraZoom);

    }
}
