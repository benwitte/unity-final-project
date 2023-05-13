using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiRun
{
    public class PlayerController : MonoBehaviour
    {
        const float gravity = -30;

        CharacterController characterController;
        GameController gameController;
        float yspeed;


        public float jumpSpeed = 15;
        public float moveSpeed = 5f;
        public float worldMoveSpeed = 10f;

        public void Start()
        {
            characterController = GetComponentInChildren<CharacterController>();
            gameController = GameController.GetCurrentController();
        }

        public void Update()
        {
            if (characterController.gameObject.transform.position.y < 0) gameController.Lose();
            if (!gameController.IsPlaying) return;

            HandleMovement();
        }

        private void HandleMovement()
        {
            var movement = transform.rotation * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (characterController.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                    yspeed = jumpSpeed;
            }
            else
            {
                yspeed += gravity * Time.deltaTime;
            }

            Vector3 velocity = (movement * Mathf.Clamp01(movement.magnitude) * moveSpeed) + (transform.rotation * Vector3.forward * worldMoveSpeed);
            velocity.y = yspeed;

            characterController.Move(velocity * Time.deltaTime);
        }

        public void Turn(bool turnRight)
        {
            transform.rotation *= Quaternion.Euler(0, turnRight ? 90 : -90, 0);
        }
    }
}