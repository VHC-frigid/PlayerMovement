using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ThirdPersonController : CustomController
{
    //hold the transform of the camera anchor
    [SerializeField] private Transform cameraTransform;

    protected override void Move(Vector3 moveDirection)
    {
        //take the input direction, and transform it based on the camera facing direction
        moveDirection = cameraTransform.TransformDirection(moveDirection);
        
        //if the player is pressing a direction
        if (moveDirection.magnitude > 0.5f)
        {
            //determine our facing direction ignoring y movement
            Vector3 facingDirection = new Vector3(moveDirection.x, 0, moveDirection.z);

            //apply the direction as our forward
            transform.forward = facingDirection;
        }
        
        base.Move(moveDirection);
    }
}
