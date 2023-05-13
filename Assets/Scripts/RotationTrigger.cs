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
            if (!(canTurn && other.tag == "Player")) return;

            var movement = Input.GetAxis("Horizontal");
            if (System.Math.Abs(movement) > float.Epsilon)
            {
                bool isRightTurn = movement > float.Epsilon;
                controller.playerController.Turn(isRightTurn);
                canTurn = false;
            }
        }
    }
}