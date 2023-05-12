using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiRun
{
    public class PlayerMovement : MonoBehaviour
    {
        const float gravity = -30;

        CharacterController characterController;
        float yspeed;

        public float jumpSpeed = 15;
        public float moveSpeed = 5f;

        public void Start()
        {
            characterController = GetComponent<CharacterController>();
        }

        public void Update()
        {
            var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (characterController.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    yspeed = jumpSpeed;
                }
            }
            else
            {
                yspeed += gravity * Time.deltaTime;
            }

            float magnitude = Mathf.Clamp01(movement.magnitude) * moveSpeed;

            Vector3 velocity = movement * magnitude;
            velocity.y = yspeed;

            characterController.Move(velocity * Time.deltaTime);
        }
    }
}