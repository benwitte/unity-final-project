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

        void Update()
        {
            score++;
            scoreLabel.text = score.ToString();
        }
    }
}