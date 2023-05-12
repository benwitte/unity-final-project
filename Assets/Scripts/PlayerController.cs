using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiRun
{
    public class PlayerController : MonoBehaviour
    {
        const float gravity = -30;

        CharacterController characterController;
        float yspeed;
        bool isRotating;


        public float jumpSpeed = 15;
        public float moveSpeed = 5f;

        public void Start()
        {
            characterController = GetComponentInChildren<CharacterController>();
        }

        public void Update()
        {
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

            Vector3 velocity = movement * Mathf.Clamp01(movement.magnitude) * moveSpeed;
            velocity.y = yspeed;

            characterController.Move(velocity * Time.deltaTime);
        }

        IEnumerator TurnCoroutine(bool turnRight)
        {
            const float lerpDuration = 0.5f;
            isRotating = true;
            float timeElapsed = 0;
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = transform.rotation * Quaternion.Euler(0, turnRight ? 90 : -90, 0);

            while (timeElapsed < lerpDuration)
            {
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            transform.rotation = targetRotation;
            isRotating = false;
        }

        public void Turn(bool turnRight)
        {
            if (isRotating) return;
            StartCoroutine(TurnCoroutine(turnRight));
        }
    }
}