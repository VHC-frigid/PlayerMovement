using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithForce : MonoBehaviour
{
    public Rigidbody rb;

    public float speed;
    public float jumpPower;

    public float sprintMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left shift"))
        {
            sprintMultiplier = 1.5f;
        
        } 
        else
        {
            sprintMultiplier = 1;

        }
        if (Input.GetKey("w"))
        {
            rb.AddForce(Vector3.forward * speed * sprintMultiplier);

        }
        if (Input.GetKey("a"))
        {
            rb.AddForce(Vector3.left * speed * sprintMultiplier);

        }
        if (Input.GetKey("d"))
        {
            rb.AddForce(Vector3.right * speed * sprintMultiplier);

        }
        if (Input.GetKey("s"))
        {
            rb.AddForce(Vector3.back * speed * sprintMultiplier);

        }
        if (Input.GetKeyDown("space"))
        {
            rb.AddForce(Vector3.up * jumpPower * sprintMultiplier, ForceMode.Impulse);

        }

















    }
}
