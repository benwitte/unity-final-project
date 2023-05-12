using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float jumpSpeed = 15;

    public float moveSpeed = 5f;

    private float gravity = -30;

    CharacterController controller;


    private Vector3 startPosition;

    private float yspeed;




    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


    
        if (controller.isGrounded)
        {

            if (Input.GetButtonDown("Jump"))
            {
                yspeed = jumpSpeed;
            }
        } else 
        {
            yspeed += gravity*Time.deltaTime;
        }


        float magnitude = Mathf.Clamp01(move.magnitude) * moveSpeed;

        Vector3 velocity = move * magnitude;
        velocity.y = yspeed;

        controller.Move(velocity * Time.deltaTime);


        /*
         if (Input.GetKey(KeyCode.A)) 
         {
            Debug.Log("left");
         }
         if (Input.GetKey(KeyCode.D)) 
         {
            Debug.Log("right");
         }
         if (Input.GetKey(KeyCode.W)) 
         {
            Debug.Log("forward");
         }
         if (Input.GetKey(KeyCode.S)) 
         {
            Debug.Log("backward");
         }
         if (Input.GetKey(KeyCode.Space)) 
         {
            Debug.Log("jump");
         }
         */
    
    }
}
