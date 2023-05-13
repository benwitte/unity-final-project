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
            controller = GameController.GetCurrentController();
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
                canTurn = false;
            }
        }
    }
}