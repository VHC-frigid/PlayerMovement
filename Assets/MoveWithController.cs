using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveWithController : MonoBehaviour
{
    // how fast our character moves
    public float speed;
    //how stong the character jump is
    public float jumpPower;
    //the component of our game object
    public float gravity;
    //the direction we should move in
    public CharacterController controller;
    //the direction we should move in
    public Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x =0;
        moveDirection.z = 0;

        if (Input.GetKey("w"))
        {
            moveDirection.z +=1;
        }
        if (Input.GetKey("s"))
        {
            moveDirection.z -= 1;
        }
        if (Input.GetKey("a"))
        {
            moveDirection.x -= 1;
        }
        if (Input.GetKey("d"))
        {
            moveDirection.x += 1;
        }
        
        //apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
        
        //if the player is on the ground
        if (controller.isGrounded)
        {
            //stop gravity from pulling us when e're on the ground        
            moveDirection.y = Mathf.Clamp(moveDirection.y,-1,float.PositiveInfinity);

            //get the jump input
            if (Input.GetKeyDown("space"))
            {
                moveDirection.y = jumpPower;
            }   
        }
        
        //Multiplying our horizontal movement by speed
        moveDirection.x = moveDirection.x * speed;
        moveDirection.z = moveDirection.z * speed;
        
        // tells the character to move
        controller.Move(moveDirection * Time.deltaTime);








    }


}
