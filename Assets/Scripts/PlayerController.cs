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
            characterController = GetComponent<CharacterController>();
        }

        public void Update()
        {
            var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            movement = transform.rotation * movement;

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