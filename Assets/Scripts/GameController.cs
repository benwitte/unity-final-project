using UnityEngine;

namespace InfiRun
{
    public class GameController : MonoBehaviour
    {
        public CameraController cameraController;
        public PlayerController playerController;
        public WorldController worldController;
        public TMPro.TextMeshProUGUI scoreLabel;

        int score = 0;

        // Not the best way of doing it but whatever lol
        public static GameController GetCurrentController()
        {
            return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        void Update()
        {
            score++;
            scoreLabel.text = score.ToString();
        }
    }
}