using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CustomController : MonoBehaviour
{
    //These values ae responsible for our player's movement
    [SerializeField] private float walkSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;

    //this will hold a reference to our camer's anchor point
    //[SerializeField] private GameObject cameraAnchor;

    [SerializeField] private LayerMask groudMask;

    private Rigidbody rb;

    //this will hold the player's inputs during an update loop
    public Vector2 inputThisFrame;

    //this will hold our calculated movement during an update an Update loop
    public Vector3 movementThisFrame;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //get the playe inputs and store in a variable       
        inputThisFrame = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        //inputthisFrame.normalised - gives us the noralized vector, without changing the vector itself
        inputThisFrame.Normalize();

        //refresh our movement to (0,0,0)
        movementThisFrame = new Vector3();

        //map our horizontal movement based on our inputs
        movementThisFrame.x = inputThisFrame.x;
        movementThisFrame.z = inputThisFrame.y;

        float speedThisFrame = walkSpeed;
        
        if (IsGrounded())
        {
            if (Input.GetButton("Sprint"))
            {
                speedThisFrame = runSpeed;
            }
            else //if only happens is reult is false
                if(Input.GetButton("Crouch"))
            {
                speedThisFrame = crouchSpeed;
            }
        }


        //multiplyth direction by our speed
        movementThisFrame *= speedThisFrame;

        //maintain current verical speed, and apply gravity
        movementThisFrame.y = rb.velocity.y - gravity * Time.deltaTime;

        //check if we're on the round
        if(IsGrounded())     //<--- remember, IsGrounded() will result in true or false
        {
            //check if the jump buton is being pressed
            if (Input.GetButtonDown("Jump"))
            {
                movementThisFrame.y = jumpPower;
            }
        }

        //run our Movw instructions, using the direction we worke out
        Move(movementThisFrame);

    }

    protected virtual void Move(Vector3 moveDirection)
    {
        rb.velocity = moveDirection;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position,Vector3.down,1.001f,groudMask);
    }
}
