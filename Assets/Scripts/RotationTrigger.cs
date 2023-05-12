using UnityEngine;

namespace InfiRun
{
    public class RotationTrigger : MonoBehaviour
    {
        bool canTurn;
        GameController controller;

        public void Start()
        {
            canTurn = false;
            controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        public void OnTriggerEnter() => canTurn = true;

        public void OnTriggerExit() => canTurn = false;

        public void OnTriggerStay(Collider other)
        {
            if (!canTurn) return;

            Debug.Log(other);
            var movement = Input.GetAxis("Horizontal");
            if (System.Math.Abs(movement) > float.Epsilon && other.tag == "Player")
            {
                Debug.Log("Test!");
                bool isRightTurn = movement > float.Epsilon;
                controller.playerController.Turn(isRightTurn);
                controller.worldController.moveSpeed = Quaternion.Inverse(Quaternion.Euler(0, isRightTurn ? -90 : 90, 0)) * controller.worldController.moveSpeed;
                canTurn = false;
            }
        }
    }
}